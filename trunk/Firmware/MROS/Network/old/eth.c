/* Mark Amato

4.7.07

Changelog:

4.13.07
line 293: changed 2nd input to function from read to 0x00

*/

/*
CONVENTION 1: defines will be ALL CAPS with words seperated by _
CONVENTION 2: functions will be one word, with caps denoting each word (ie WriteOp)

*/


//include register defs

#include <ethdefs.h>
// #include <spi.h>
#include <delay.h>
#include <stdio.h>

//bruce defs
#define begin {
#define end }

//SPI defs
#define SPE 6
#define SPIF 7
#define MSTR 4
#define SPI2X 0
// these defines might be moved to its own ehader later.  who knows?
//Enc28j60 connected to SPI, which is portb.
#define ETH_PORT PORTB
#define ETH_DDR  DDRB

// SPI defines
#define ETH_CS   4
#define ETH_MOSI 5
#define ETH_MISO 6
#define ETH_SCK  7

//Chip Select routine.  Note that Chip Select is inverse logic (low=on, high=off).  This won't drive me crazy or anything.
#define CS_ON ETH_PORT = ETH_PORT & ~(1<<ETH_CS)
#define CS_OFF ETH_PORT = ETH_PORT | (1<<ETH_CS)

unsigned char i;
unsigned char junk; //junk value for SPI() function.  JUNK SHOULD NEVER BE USED AS A REAL VALUE HOLDER.  It's a good idiot check.
unsigned char CurrentBank; //CurrentBank is the present bank of registers and allows for register access switching
unsigned int RxPacketPtr;

unsigned char spi(unsigned char data)
begin
  SPDR = data;
  while(!(SPSR & (1<<SPIF)));
  return(SPDR);
end


/* ENC28J60 Instruction Set

From page 26 of the datasheet:
                          BYTE 0          BYTE1
                         opcode argument  data
Read Control Reg  (RCR)   0 0 0 a a a a a NONE
Read Buffer Mem   (RBM)   0 0 1 1 1 0 1 0 NONE
Write Control Reg (WCR)   0 1 0 a a a a a d d d d d d d d
Write Buffer Mem  (WBM)   0 1 1 1 1 0 1 0 d d d d d d d d
Bit Field Set     (BFS)   1 0 0 a a a a a d d d d d d d d
Bit Field Clear   (BFC)   1 0 1 a a a a a d d d d d d d d
System Reset    (RESET)   1 1 1 1 1 1 1 1 NONE

a = control reg, d = data.

Option one for writing this would be to make one function that does all of this stuff appropriately and is essentially a large case statement.
   This would be most portable and easy to use when programming, although slower due to the use of a case statement versus "intelligent"
   programmer selection of specific routines.
Option two, and what has been used in the official Microchip implementation as well as the two GCC implementations, is a variety of functions
   designed to do this sort of thing.  Basically, one function per op, which is not really efficient.

Right now I'm going to do option 2 on the basis that although I understand how to handle a switch statement for nodata operations and don't
think the switch evaluation would be a big deal, I don't know how to handle recursive calls (reading a bank, for example) or how to handle
multiple byte writes.  Ultimately, writing a large routine requires arguably more programmer savvy since it will be easy to misinterpret commands.
Packet reads and writes will be their own function anyway, so as long as I know how to do reads and writes to registers that's all that matters.
-Amato 4.7.07
*/
//Opcodes
#define ETH_OP_RCR   0x00
#define ETH_OP_RBM   0x3A
#define ETH_OP_WCR   0x40
#define ETH_OP_WBM   0x7A
#define ETH_OP_BFS   0x80
#define ETH_OP_BFC   0xA0
#define ETH_OP_RESET 0xFF

//Argument mask: low 5 bits of bank definition from ethdefs.h
#define ETH_ARG_MASK 0x1F
/*
Bank mask:bits 8,9 from ethdefs.h.  This tells the system which bank of registers to write and allows addressing from within one 8 bit op/address
byte while having more than 32 registers.
*/
#define ETH_BANK_MASK 0x300

//MAC/MII register mask: bit 10 from ethdefs.h
#define ETH_MAC_MII_MASK 0x4000

//Transmit buffer length in hex (bytes)
#define ETH_TX_LEN 0x0640
#define ETH_TX_START 0x19BF

/*

ReadOp works by sending the  op and control reg addy (argument)  then reads in a single byte of data.
Some goofy stuff here: If reading a MAC or MII reg, need to pad by reading a dummy byte before the real byte.  Why Microchip
decided this was a good idea is beyond my comprehesion, maybe there's a timing thing, or maybe they just hate engineers.
Wait, they make the PIC.  They hate engineers.

Note that this used to be ReadControlReg and only used to do RCR commands, but the way that the register banks need
to be addressed requires a generalized version.  ReadControlReg can be called as a shell to this.

*/


unsigned char ReadOp(unsigned char op, unsigned int argument)
begin
   // local variable for real data.  See note on junk var in header.
   unsigned char readdata;

   //Bring CS low... by turning it on.  I hate you, inverse signalling.
   CS_ON;
   //Build first byte: top 3 bits of opcode, bottom 5 bits of argument.  Then hit the SPI.
   junk = spi( op | (argument & ETH_ARG_MASK) );
   //check for MAC/MII and do a dummy read if it is
   if( (argument & ETH_MAC_MII_MASK) ==1 )
   begin
      junk = spi(0x00);
   end
   //Read data byte
   readdata = spi(0x00);
   //Bring CS high
   CS_OFF;
   return(readdata);
end

/*
ReadControlReg is the shell built on top of ReadOp and is used for ease of calling.
*/

unsigned char ReadControlReg(unsigned int argument)
begin
   int datareturn;
   datareturn = ReadOp(ETH_OP_RCR,argument);
   return(datareturn);
end

/*
ReadBuffer works by sending the Read Buffer Memory op, then read each byte of data until out of data to read.
*/

void ReadBuffer(unsigned int length, unsigned char* data)
begin
   //CS low
   CS_ON;
   //send ReadBuffer op
   spi(ETH_OP_RBM);
   //for (length) bytes
   while(length--)
   begin
      *data = spi(0x00);  //write read byte and write it to the memory address data is pointing to.
      data++;             //increment.
   end
   //CS High
   CS_OFF;
   *data = '/0';



end

/*

WriteOp works by sending the  specificop and control reg addy (argument)  then sends out a single byte of data.
Note that this used to be WriteControlOp and only used to do WCR commands, but the way that the register banks need
to be addressed requires a generalized version.  WriteControlOp can be called as a shell to this.

*/

void WriteOp(unsigned char op, unsigned int argument, unsigned char data)
begin
   //Bring CS low
   CS_ON;
   //Build first byte: top 3 bits of opcode, bottom 5 bits of argument.  Then hit the SPI.
   SPDR = op | (argument & ETH_ARG_MASK);
   while(!(SPSR & (1<<SPIF))) ;
   //Write data byte
   SPDR = data;
   while(!(SPSR & (1<<SPIF))) ;
   //Bring CS high
   CS_OFF;
end


/*
WriteControlReg is a shell built on top of WriteOp and is used for ease of calling.
*/

void WriteControlReg(unsigned int argument,unsigned char data)
begin
   WriteOp(ETH_OP_WCR,argument,data);
end

/*
BitFieldSet is a shell built on top of WriteOp and is used for ease of calling.

BitFieldSet works by sending the Bit Field Set op and register (argument)  then sends out a single byte of data.
Two notes:  One, this can't be used on PHY registers, or buffer memory (use the other commands for that).
Two: there's more registers to write than there are bits for arguments.  Be sure to have the correct bank set.

*/

void BitFieldSet(unsigned int argument,unsigned char data)
begin
   WriteOp(ETH_OP_BFS,argument,data);
end

/*
BitFieldClear is a shell built on top of WriteOp and is used for ease of calling.
*/

void BitFieldClear(unsigned int argument,unsigned char data)
begin
   WriteOp(ETH_OP_BFC,argument,data);
end


/*
WriteBuffer works by sending the Write Buffer Memory op, then sending each byte of data until out of data.
*/

void WriteBuffer(unsigned int length, unsigned char* packet)
begin
   //Bring CS low
   CS_ON;
   //Send Write Buffer op
   junk = spi(ETH_OP_WBM);
   while(length--)    //postdecrement
   begin
      /*starts with zeroth byte of data by dereferencing the pointer and grabbing the value.  Great trick for zooming through an array
      without needing to copy the whole array into the function or declare it as a global.
      */
      junk=spi(*packet);
      // increment pointer by one
      packet++;
   end
   //bring CS high
   CS_OFF;
end


/*
SetBank sets the correct bank of the particular register that is being accessed.  If the bank is already selected, it skips the commands.
*/


void SetBank(unsigned int regaddress)
begin
   if((regaddress & ETH_BANK_MASK) != CurrentBank)//if the bank is not selected, change it.  otherwise, no need to change bank.
   begin
      //clear the bank select bits in ECON1
      BitFieldClear(ECON1,(ECON1_BSEL1 | ECON1_BSEL0));
      //calculate the correct bank and save it for later checking
      CurrentBank = ((regaddress & ETH_BANK_MASK)>>8);
      //write the correct bank
      BitFieldSet(ECON1,(CurrentBank & 0x03));
   end
end

/*

Write is a simple command that writes the register we want it to.  It cannot be used with PHY since those are 16 bit writes.
For PHY writes, use WritePhy. As usual, memory is its own little world and shouldn't be touched with either command.  MAC is weird
and will be addressed later as it's own function (it's backwards)!
*/

void Write(unsigned int regaddress, unsigned char data)
begin
   // check/set the bank
   SetBank(regaddress);
   // write the register with data
   WriteControlReg(regaddress,data);
end

/*

Write is a simple command that writes the register we want it to. It cannot be used with PHY since it is a 16 bit read.
As usual, memory is its own little world and shouldn't be touched with either command.  MAC is weird
and will be addressed later as it's own function (it's backwards)!

*/

unsigned char Read(unsigned int regaddress)
begin
   int returndata; //returned data
   // check/set the bank
   SetBank(regaddress);
   // read the register data
   returndata = ReadControlReg(regaddress);
   return(returndata);
end
/*

WritePhy is a simple command that writes the PHY register we want it to.  It cannot be used with normal registers since those are
8 bit writes. For normal writes, use Write. As usual, memory is its own little world and shouldn't be touched with either command.
MAC writes are weird and will be addressed later (it's backwards)!

*/

void WritePhy(unsigned int regaddress, unsigned int data)
begin
   // write MIREGADR with the PHY/MII address
   Write(MIREGADR, regaddress);
   // write the low bit of data
   Write(MIWRL, (data & 0x00ff));
   //write the high bit of data
   Write(MIWRH, (data & 0xff00)>>8); //yeah, the mask is completely unneccesary, but it's helpful for reminding me what is going on.  I'll take it out in the production code.
   //check to see if the data has been moved from MIWRL and MIWRH to the desired register; burn cycles until it's done
//   PORTA= 0xff;
   while(Read(MISTAT) & MISTAT_BUSY)
   begin
//      PORTA = 0x00;
      delay_ms(20); //write has not completed; burn cycles!
   end
//   PORTA = 0xFF;
end

unsigned int ReadPhy(unsigned int regaddress)
begin
   int temp;
   //set the address
   Write(MIREGADR,regaddress);
   //set the load bit
   WritePhy(MICMD, 1);
   //clear the load bit
   WritePhy(MICMD, 0);
   //read MIWRL
   temp = Read(MIWRH);
   temp = (temp<<8)+((int)Read(MIWRL));
   return temp;
end
/*
ResetEth does what you think it would: Resets the ethernet chip in software.  It builds in the required burn time before the chip sets up, since
CLKRDY does not work. B5 Errata point 1 outlines the requirement of at least a 1ms wait... I got plenty of time, so I'll burn 20ms.
*/

void ResetEth(void)
begin
     spi(0xff);            //FF = reset
     delay_ms(20);
end



void InitEthernet(unsigned char* macaddress)
begin
 

   /* I/O initialization section
      Port is defined by ETH_DDR (default: PortB).
      ETH_CS -->
      ETH_MOSI -->
      ETH_MISO <--
      ETH_SCK -->
   */
   //Initialize DDRB as above
   ETH_DDR = ETH_DDR | (1<<ETH_CS) | (1<<ETH_MOSI) & ~(1<<ETH_MISO) | (1<<ETH_SCK) ;
   //Bring SS High, turning off Chip Select.
   CS_OFF;
   //Set MOSI and SCK low
   ETH_PORT = ETH_PORT & ~(1<<ETH_MOSI) & ~(1<<ETH_SCK);

   //SPI Section
   // Enable SPI and Master Comms
   SPCR = (1<<SPE)|(1<<MSTR);
   /* 2xSPI speed should be enabled as spec'd by datasheet.  MEGA32 will be operating at 12.5MHz during actual operation
      (it's 6.25 during start-up) so we want to be able to get the ENC28J60 data at the double rate.
   */
   SPSR = SPSR | (1<<SPI2X);
   //Initialization!

   // This section follows the initialization routine suggested by Section 6 of the ENC28J60 exactly.

   //reset
   ResetEth();

   Write(ECOCON, 0b010);

   //Receive Buffer startup
   //Write the start addy (0x0000 is suggested by the b5 errata datasheet)

   Write(ERXSTL,0x00);

   Write(ERXSTH,0x00);
   //Write the starting receive pointer addy
   Write(ERXRDPTL,0x00);
   Write(ERXRDPTH,0x00);
   
 
   //Make the MEGA32 RX pointeraddy pointer while we're here
   RxPacketPtr=0x0000;


   /* Write the ending rx addy.  Memory is 8192 bytes (0x1fff bytes).
   We need to leave >1500 bytes available for the Transmit buffer since we'll be using the maximum packet size of 1500bytes.  With 100 byte pad,
   We need a TX buffer of 1600bytes or 0x0640 bytes.  This value is defined above as #ETH_TX_LEN.  Therefore, ERXND must be 0x1fff-640-1
   since the TX buffer starts at 0x1fff-640. We'll define this as #ETH_TX_START, and it'll get used later.
   */
   Write(ERXNDL,((ETH_TX_START-1) & 0xFF));
   Write(ERXNDH,((ETH_TX_START-1)>>8));
   //blinkLED();
   //Write the starting tx addy.
   Write(ETXSTL,(ETH_TX_START & 0xFF));
   Write(ETXSTH,(ETH_TX_START >> 8));
   //blinkLED();
   //Write the ending TX addy
   Write(ETXNDL,(0x1fff & 0xff));
   Write(ETXNDH,(0x1fff >> 8 ));
 

//    //Packet filtering... BANK1!
//
//      This is a tough one.  In order to not overload our system, we want to do packet filtering.  Ideally, we only want to allow packets unicast
//    to our MAC address.  That said, we also want to provide for ARP packets so that we can maintain an IP - MAC address mapping at the switch level.
//    ARP works like this:
//    - Host 1 sends a packet destined for Host 2 to the ARP module in the switch.
//    - ARP module looks up the ARP table entry to resolve the IP.
//    - If the supplied IP address is in the ARP cache, it is resolved into the MAC addres.
//    - If the ARP module can't find the entry for the IP in the cach, it sends an ARP request packet to the network.
//    - If Host 2 receives the ARP rquest packet and the IP is the host's IP, then HOST2 sends an ARP reply packet with it's MAC.
//    - If not, Host 2 discards the packet.
//
//    Okay, so what does this mean?  This means that we need to allow unicast packets OR ARP request packets.  We should also do CRC checking.
//
//
  Write(ERXFCON,0x00);//(ERXFCON_UCEN|ERXFCON_PMEN));
 

//
//
//    ARP requests are those with MAC addresses of ff:ff:ff:ff:ff:ff.  According to http://www.geocities.com/SiliconValley/Vista/8672/network/arp.html#A1,
//    we need to use the first bytes 0-5 of the header (that's the mac address) as well as header bytes 13 and 14, which are 06 and 08 respectively
//    with ARP.  In binary, the bits to read are 0011 0000 0011 1111, or 303f.
//    This garners the one's complement sum of 0xFF + 0xFF + 0xFF + 0xFF + 0xFF + 0xFF = 0x05FA; Adding 0x06 and 0x08 we get 0b0000 0110 0000 1000.
//    The 1's complement of this is 0b 1111 1001 1111 0111, or 0xF9F7.  This value is 16-bit backward within the registers and becomes 0xF7F9.
//
//
   //no offset, so don't set pattern match offset EMPO

   //Set pattern match bytes
   Write(EPMM0, 0x3f);
   Write(EPMM1, 0x30);
   //write IP checksum value
   Write(EPMCSL,0xf9);
   Write(EPMCSH,0xf7);

   // MAC settings
//
//    /* In the datasheet, it says about polling the CLKRDY bit, but the errata sheet says that the bit doesn't work.  Well, we've waited a ton of time
//    with that 20ms burn and that should be more than sufficient for the 300microsecond setup time for the clock.  Something to check, at least.
//
//    */

   //Set MARXEN as well as TXPAUS and RXPAUS for frame enable and flow control.
   Write(MACON1,(MACON1_MARXEN|MACON1_TXPAUS|MACON1_RXPAUS));
 
   //if(Read(MACON1)!=(MACON1_MARXEN|MACON1_TXPAUS|MACON1_RXPAUS)) while(1) PORTC = Read(MACON1);
   /*Write PADCFG, and TXCRCEN bits in MACON 3 for automatic padding and CRC appendage.  Datasheet suggest FULDPX for full duplex mode but
   MEGA hardware cannot deal with this mode.  We also set FRMLNEN for frame length status reporting.  Don't touch values already automatically
   assigned... so use BitFieldSet.
   */
   BitFieldSet(MACON3,(MACON3_PADCFG0|MACON3_TXCRCEN|MACON3_FRMLNEN));
  
   //Don't bother with MACON4 as we don't care about 802.3 compliance and would like for transmission abortion after deferral limit is hit

   //Maximum framelength accepted: For ethernet, it's 1500bytes.  We need to pad that
   //a bit since the ethernet controller is adding extra bytes to the incoming packet.
   // Hey, why don't we set it equal to the maximum size of our transmit buffer?  THAT MAKES SENSE TO ME!
   Write(MAMXFLL,(ETH_TX_LEN & 0xFF));
   Write(MAMXFLL,(ETH_TX_LEN >> 8));

   //Interpacket gap, back-to-back
   Write(MABBIPG, 0x12);

   //Interpacket gap, non-back-to-back
   Write(MAIPGL,0x12);
   Write(MAIPGH,0x0C);

   // Write the MAC address.  Backwards.  Yeeeaaah.
   Write(MAADR6,macaddress[0]);
   Write(MAADR5,macaddress[1]);
   Write(MAADR4,macaddress[2]);
   Write(MAADR3,macaddress[3]);
   Write(MAADR2,macaddress[4]);
   Write(MAADR1,macaddress[5]);

   // PHCON1 has zeroes written to ensure half-duplex operation.
   WritePhy(PHCON1,0x0000);
   // PHCON2 should have HDLDIS set to prevent half-duplex loopback.
   WritePhy(PHCON2,PHCON2_HDLDIS);

   //burn cycles while waiting for PHY write to be done by switching banks.  Also need to turn the chip on anyway which requires EIE and ECON1.
   //NOTE: the setbank might be unneccesary since EIE and ECON1 are mappable from any bank.  Try 'er later.

   SetBank(ECON1);

   // Enable Interrupts whenever a packet is received:
   BitFieldSet(EIE,(EIE_PKTIE | EIE_INTIE));
   PORTC = ~ Read(EIE);
   delay_ms(1000);
   //PORTC= 0xff  ;
   // Enable packets to be received:
   BitFieldSet(ECON1,ECON1_RXEN);
   //delay_ms(1000);
   PORTC = ~ Read(ECON1);
   delay_ms(1000);
   PORTC= 0xff;
   // On reset, it was operating at clock/4.  Set clock output to half-clock:

   //burn cycles to wait for MEGA32 to stabilize
   //delay_ms(6000);
   //WritePhy(PHLCON, 0b0011101110110000);
   WritePhy(PHLCON, 0x3246);
//    delay_ms(6000);


end



/*
TransmitPacket transmits a packet of length bytes.  It follows the ordering in 7.1 in the datasheet, unlike the implementation used in GCC and
PIC.  Not sure why.  Something to check.
*/

void TransmitPacket(unsigned int length, unsigned char *data)
begin
   //reset write pointer to start of transmit buffer
   Write(EWRPTL,(ETH_TX_START & 0xFF));
   Write(EWRPTH,(ETH_TX_START >> 8));

   //Write Per Packet control byte: use MACON3 settings
   WriteOp(ETH_OP_WBM,0,0x00);
   WriteBuffer(length,data);

   //set TXND pointer to end of packet (don't want to transmit extra garbage bytes
   Write(ETXNDL,((ETH_TX_START+length) & 0xFF));
   Write(ETXNDH,((ETH_TX_START+length) >> 8));

   //Send the buffer
   BitFieldSet(ECON1, ECON1_TXRTS);
   //Reset transmit bit in case of logic problem (Errata 12)
   if((Read(EIR) & EIR_TXERIF)) BitFieldClear(ECON1,ECON1_TXRTS);
   //while(1) PORTC = ~length;
end

/*
ReceivePacket reads the "oldest" packet (the ethernet device is FIFO assuming that it's not clobbering its own memory, which shouldn't happen
according to the datasheet.  Then again, this is Microchip, and they apparently can't do anything right.
*/

unsigned int ReceivePacket(unsigned int maximumlength, unsigned char *packet)
begin
   unsigned int receivestatusvector;
   unsigned int receivedbytecount;
   //if(Read(EPKTCNT)!= 0 ) while(1) PORTC = ~Read(EPKTCNT);
   //   PORTA = PORTA ^ 0xff;
   //check to see if there's a packet received in the first place:
   if(Read(EPKTCNT) == 0)
   begin
      receivedbytecount = 0;  //received packet count is zero.  Forget it.
      return(receivedbytecount);
   end 
   else
   begin
      //PORTC = Read(EPKTCNT);
      //set read pointer to start of packet
      Write(ERDPTL, ( RxPacketPtr & 0xff));
      Write(ERDPTH, (RxPacketPtr>>8));
   /* Figure 7.3 shows us what is going on with the received packet as we read it.
   Byte                   Descriptor
   0-1                    NextPacketPointer
   2-5                    Receive status vector
   6-maxlength            Packet data
   maxlength-maxlength+3  CRC in reverse bit order
   */
      //snag the next packet pointer and save it for next packet read
      RxPacketPtr = ReadOp(ETH_OP_RBM,0);
      RxPacketPtr = RxPacketPtr | (((int)( ReadOp(ETH_OP_RBM,0))) <<8);
     // while(1) PORTC = ~RxPacketPtr;

      // receive status vector bits 0-15 are the number of bytes of this packet.  We'll separate these as they're kinda important.
      receivedbytecount = ReadOp(ETH_OP_RBM,0);
      receivedbytecount = receivedbytecount | (((int)( ReadOp(ETH_OP_RBM,0)))<<8);
      //load the rest of the receive status vector
      receivestatusvector = ReadOp(ETH_OP_RBM,0);
      receivestatusvector = receivestatusvector | (((int) ( ReadOp(ETH_OP_RBM,0)))<<8);
      //check the RX_OK! bit and dump out if not set:
      if((receivestatusvector & 0x80)==0) 
      begin 
         receivedbytecount = 0;
      end
      else //RX is OK!  Let's read! Yay.
      begin
         
         //received byte count includes CRC field, which we don't care about as we aren't doing active crc checking.  We will poll the RX_OK! bit later.
         receivedbytecount = receivedbytecount - 4;
         //check to make sure we aren't too big, and set to max length if we are
         if(receivedbytecount>maximumlength-1) receivedbytecount=maximumlength-1;

         //read the entire buffer
         ReadBuffer(receivedbytecount,packet);
      end
      //move pointer to start of next received packet, allowing the memory we just read to be clobbered:

      Write(ERXRDPTL,(RxPacketPtr & 0xFF));
      Write(ERXRDPTL,(RxPacketPtr >> 8));
      // packet count decrement, see page 16
      BitFieldSet(ECON2, ECON2_PKTDEC);
      //PORTC = PORTC ^ 0xa0;
      return(receivedbytecount);
   end
  
end

void blinkLED(void)
begin
end
