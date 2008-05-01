










 














 













 




 



  

















 






 

#pragma language=extended

 
 
 























 

__io union { unsigned char TWBR; struct { unsigned char TWBR_Bit0:1, TWBR_Bit1:1, TWBR_Bit2:1, TWBR_Bit3:1, TWBR_Bit4:1, TWBR_Bit5:1, TWBR_Bit6:1, TWBR_Bit7:1; }; } @ 0x00;      
__io union { unsigned char TWSR; struct { unsigned char TWSR_Bit0:1, TWSR_Bit1:1, TWSR_Bit2:1, TWSR_Bit3:1, TWSR_Bit4:1, TWSR_Bit5:1, TWSR_Bit6:1, TWSR_Bit7:1; }; } @ 0x01;      
__io union { unsigned char TWAR; struct { unsigned char TWAR_Bit0:1, TWAR_Bit1:1, TWAR_Bit2:1, TWAR_Bit3:1, TWAR_Bit4:1, TWAR_Bit5:1, TWAR_Bit6:1, TWAR_Bit7:1; }; } @ 0x02;      
__io union { unsigned char TWDR; struct { unsigned char TWDR_Bit0:1, TWDR_Bit1:1, TWDR_Bit2:1, TWDR_Bit3:1, TWDR_Bit4:1, TWDR_Bit5:1, TWDR_Bit6:1, TWDR_Bit7:1; }; } @ 0x03;      
__io union { unsigned short ADC; struct { unsigned char ADC_Bit0:1, ADC_Bit1:1, ADC_Bit2:1, ADC_Bit3:1, ADC_Bit4:1, ADC_Bit5:1, ADC_Bit6:1, ADC_Bit7:1; unsigned char ADC_Bit8:1, ADC_Bit9:1, ADC_Bit10:1, ADC_Bit11:1, ADC_Bit12:1, ADC_Bit13:1, ADC_Bit14:1, ADC_Bit15:1; }; struct { unsigned char ADCL; unsigned char ADCH; }; struct { unsigned char ADCL_Bit0:1, ADCL_Bit1:1, ADCL_Bit2:1, ADCL_Bit3:1, ADCL_Bit4:1, ADCL_Bit5:1, ADCL_Bit6:1, ADCL_Bit7:1; unsigned char ADCH_Bit0:1, ADCH_Bit1:1, ADCH_Bit2:1, ADCH_Bit3:1, ADCH_Bit4:1, ADCH_Bit5:1, ADCH_Bit6:1, ADCH_Bit7:1; }; } @ 0x04;      
__io union { unsigned char ADCSRA; struct { unsigned char ADCSRA_Bit0:1, ADCSRA_Bit1:1, ADCSRA_Bit2:1, ADCSRA_Bit3:1, ADCSRA_Bit4:1, ADCSRA_Bit5:1, ADCSRA_Bit6:1, ADCSRA_Bit7:1; }; } @ 0x06;      
__io union { unsigned char ADMUX; struct { unsigned char ADMUX_Bit0:1, ADMUX_Bit1:1, ADMUX_Bit2:1, ADMUX_Bit3:1, ADMUX_Bit4:1, ADMUX_Bit5:1, ADMUX_Bit6:1, ADMUX_Bit7:1; }; } @ 0x07;      
__io union { unsigned char ACSR; struct { unsigned char ACSR_Bit0:1, ACSR_Bit1:1, ACSR_Bit2:1, ACSR_Bit3:1, ACSR_Bit4:1, ACSR_Bit5:1, ACSR_Bit6:1, ACSR_Bit7:1; }; } @ 0x08;      
__io union { unsigned char UBRRL; struct { unsigned char UBRRL_Bit0:1, UBRRL_Bit1:1, UBRRL_Bit2:1, UBRRL_Bit3:1, UBRRL_Bit4:1, UBRRL_Bit5:1, UBRRL_Bit6:1, UBRRL_Bit7:1; }; } @ 0x09;      
__io union { unsigned char UCSRB; struct { unsigned char UCSRB_Bit0:1, UCSRB_Bit1:1, UCSRB_Bit2:1, UCSRB_Bit3:1, UCSRB_Bit4:1, UCSRB_Bit5:1, UCSRB_Bit6:1, UCSRB_Bit7:1; }; } @ 0x0A;      
__io union { unsigned char UCSRA; struct { unsigned char UCSRA_Bit0:1, UCSRA_Bit1:1, UCSRA_Bit2:1, UCSRA_Bit3:1, UCSRA_Bit4:1, UCSRA_Bit5:1, UCSRA_Bit6:1, UCSRA_Bit7:1; }; } @ 0x0B;      
__io union { unsigned char UDR; struct { unsigned char UDR_Bit0:1, UDR_Bit1:1, UDR_Bit2:1, UDR_Bit3:1, UDR_Bit4:1, UDR_Bit5:1, UDR_Bit6:1, UDR_Bit7:1; }; } @ 0x0C;      
__io union { unsigned char SPCR; struct { unsigned char SPCR_Bit0:1, SPCR_Bit1:1, SPCR_Bit2:1, SPCR_Bit3:1, SPCR_Bit4:1, SPCR_Bit5:1, SPCR_Bit6:1, SPCR_Bit7:1; }; } @ 0x0D;      
__io union { unsigned char SPSR; struct { unsigned char SPSR_Bit0:1, SPSR_Bit1:1, SPSR_Bit2:1, SPSR_Bit3:1, SPSR_Bit4:1, SPSR_Bit5:1, SPSR_Bit6:1, SPSR_Bit7:1; }; } @ 0x0E;      
__io union { unsigned char SPDR; struct { unsigned char SPDR_Bit0:1, SPDR_Bit1:1, SPDR_Bit2:1, SPDR_Bit3:1, SPDR_Bit4:1, SPDR_Bit5:1, SPDR_Bit6:1, SPDR_Bit7:1; }; } @ 0x0F;      
__io union { unsigned char PIND; struct { unsigned char PIND_Bit0:1, PIND_Bit1:1, PIND_Bit2:1, PIND_Bit3:1, PIND_Bit4:1, PIND_Bit5:1, PIND_Bit6:1, PIND_Bit7:1; }; } @ 0x10;      
__io union { unsigned char DDRD; struct { unsigned char DDRD_Bit0:1, DDRD_Bit1:1, DDRD_Bit2:1, DDRD_Bit3:1, DDRD_Bit4:1, DDRD_Bit5:1, DDRD_Bit6:1, DDRD_Bit7:1; }; } @ 0x11;      
__io union { unsigned char PORTD; struct { unsigned char PORTD_Bit0:1, PORTD_Bit1:1, PORTD_Bit2:1, PORTD_Bit3:1, PORTD_Bit4:1, PORTD_Bit5:1, PORTD_Bit6:1, PORTD_Bit7:1; }; } @ 0x12;      
__io union { unsigned char PINC; struct { unsigned char PINC_Bit0:1, PINC_Bit1:1, PINC_Bit2:1, PINC_Bit3:1, PINC_Bit4:1, PINC_Bit5:1, PINC_Bit6:1, PINC_Bit7:1; }; } @ 0x13;      
__io union { unsigned char DDRC; struct { unsigned char DDRC_Bit0:1, DDRC_Bit1:1, DDRC_Bit2:1, DDRC_Bit3:1, DDRC_Bit4:1, DDRC_Bit5:1, DDRC_Bit6:1, DDRC_Bit7:1; }; } @ 0x14;      
__io union { unsigned char PORTC; struct { unsigned char PORTC_Bit0:1, PORTC_Bit1:1, PORTC_Bit2:1, PORTC_Bit3:1, PORTC_Bit4:1, PORTC_Bit5:1, PORTC_Bit6:1, PORTC_Bit7:1; }; } @ 0x15;      
__io union { unsigned char PINB; struct { unsigned char PINB_Bit0:1, PINB_Bit1:1, PINB_Bit2:1, PINB_Bit3:1, PINB_Bit4:1, PINB_Bit5:1, PINB_Bit6:1, PINB_Bit7:1; }; } @ 0x16;      
__io union { unsigned char DDRB; struct { unsigned char DDRB_Bit0:1, DDRB_Bit1:1, DDRB_Bit2:1, DDRB_Bit3:1, DDRB_Bit4:1, DDRB_Bit5:1, DDRB_Bit6:1, DDRB_Bit7:1; }; } @ 0x17;      
__io union { unsigned char PORTB; struct { unsigned char PORTB_Bit0:1, PORTB_Bit1:1, PORTB_Bit2:1, PORTB_Bit3:1, PORTB_Bit4:1, PORTB_Bit5:1, PORTB_Bit6:1, PORTB_Bit7:1; }; } @ 0x18;      
__io union { unsigned char PINA; struct { unsigned char PINA_Bit0:1, PINA_Bit1:1, PINA_Bit2:1, PINA_Bit3:1, PINA_Bit4:1, PINA_Bit5:1, PINA_Bit6:1, PINA_Bit7:1; }; } @ 0x19;      
__io union { unsigned char DDRA; struct { unsigned char DDRA_Bit0:1, DDRA_Bit1:1, DDRA_Bit2:1, DDRA_Bit3:1, DDRA_Bit4:1, DDRA_Bit5:1, DDRA_Bit6:1, DDRA_Bit7:1; }; } @ 0x1A;      
__io union { unsigned char PORTA; struct { unsigned char PORTA_Bit0:1, PORTA_Bit1:1, PORTA_Bit2:1, PORTA_Bit3:1, PORTA_Bit4:1, PORTA_Bit5:1, PORTA_Bit6:1, PORTA_Bit7:1; }; } @ 0x1B;      
__io union { unsigned char EECR; struct { unsigned char EECR_Bit0:1, EECR_Bit1:1, EECR_Bit2:1, EECR_Bit3:1, EECR_Bit4:1, EECR_Bit5:1, EECR_Bit6:1, EECR_Bit7:1; }; } @ 0x1C;      
__io union { unsigned char EEDR; struct { unsigned char EEDR_Bit0:1, EEDR_Bit1:1, EEDR_Bit2:1, EEDR_Bit3:1, EEDR_Bit4:1, EEDR_Bit5:1, EEDR_Bit6:1, EEDR_Bit7:1; }; } @ 0x1D;      
__io union { unsigned short EEAR; struct { unsigned char EEAR_Bit0:1, EEAR_Bit1:1, EEAR_Bit2:1, EEAR_Bit3:1, EEAR_Bit4:1, EEAR_Bit5:1, EEAR_Bit6:1, EEAR_Bit7:1; unsigned char EEAR_Bit8:1, EEAR_Bit9:1, EEAR_Bit10:1, EEAR_Bit11:1, EEAR_Bit12:1, EEAR_Bit13:1, EEAR_Bit14:1, EEAR_Bit15:1; }; struct { unsigned char EEARL; unsigned char EEARH; }; struct { unsigned char EEARL_Bit0:1, EEARL_Bit1:1, EEARL_Bit2:1, EEARL_Bit3:1, EEARL_Bit4:1, EEARL_Bit5:1, EEARL_Bit6:1, EEARL_Bit7:1; unsigned char EEARH_Bit0:1, EEARH_Bit1:1, EEARH_Bit2:1, EEARH_Bit3:1, EEARH_Bit4:1, EEARH_Bit5:1, EEARH_Bit6:1, EEARH_Bit7:1; }; } @ 0x1E;      
__io union { unsigned char UBRRH; unsigned char UCSRC; struct { unsigned char UBRRH_Bit0:1, UBRRH_Bit1:1, UBRRH_Bit2:1, UBRRH_Bit3:1, UBRRH_Bit4:1, UBRRH_Bit5:1, UBRRH_Bit6:1, UBRRH_Bit7:1; }; struct { unsigned char UCSRC_Bit0:1, UCSRC_Bit1:1, UCSRC_Bit2:1, UCSRC_Bit3:1, UCSRC_Bit4:1, UCSRC_Bit5:1, UCSRC_Bit6:1, UCSRC_Bit7:1; }; } @ 0x20; 
 
 
__io union { unsigned char WDTCR; struct { unsigned char WDTCR_Bit0:1, WDTCR_Bit1:1, WDTCR_Bit2:1, WDTCR_Bit3:1, WDTCR_Bit4:1, WDTCR_Bit5:1, WDTCR_Bit6:1, WDTCR_Bit7:1; }; } @ 0x21;      
__io union { unsigned char ASSR; struct { unsigned char ASSR_Bit0:1, ASSR_Bit1:1, ASSR_Bit2:1, ASSR_Bit3:1, ASSR_Bit4:1, ASSR_Bit5:1, ASSR_Bit6:1, ASSR_Bit7:1; }; } @ 0x22;      
__io union { unsigned char OCR2; struct { unsigned char OCR2_Bit0:1, OCR2_Bit1:1, OCR2_Bit2:1, OCR2_Bit3:1, OCR2_Bit4:1, OCR2_Bit5:1, OCR2_Bit6:1, OCR2_Bit7:1; }; } @ 0x23;      
__io union { unsigned char TCNT2; struct { unsigned char TCNT2_Bit0:1, TCNT2_Bit1:1, TCNT2_Bit2:1, TCNT2_Bit3:1, TCNT2_Bit4:1, TCNT2_Bit5:1, TCNT2_Bit6:1, TCNT2_Bit7:1; }; } @ 0x24;      
__io union { unsigned char TCCR2; struct { unsigned char TCCR2_Bit0:1, TCCR2_Bit1:1, TCCR2_Bit2:1, TCCR2_Bit3:1, TCCR2_Bit4:1, TCCR2_Bit5:1, TCCR2_Bit6:1, TCCR2_Bit7:1; }; } @ 0x25;      
__io union { unsigned short ICR1; struct { unsigned char ICR1_Bit0:1, ICR1_Bit1:1, ICR1_Bit2:1, ICR1_Bit3:1, ICR1_Bit4:1, ICR1_Bit5:1, ICR1_Bit6:1, ICR1_Bit7:1; unsigned char ICR1_Bit8:1, ICR1_Bit9:1, ICR1_Bit10:1, ICR1_Bit11:1, ICR1_Bit12:1, ICR1_Bit13:1, ICR1_Bit14:1, ICR1_Bit15:1; }; struct { unsigned char ICR1L; unsigned char ICR1H; }; struct { unsigned char ICR1L_Bit0:1, ICR1L_Bit1:1, ICR1L_Bit2:1, ICR1L_Bit3:1, ICR1L_Bit4:1, ICR1L_Bit5:1, ICR1L_Bit6:1, ICR1L_Bit7:1; unsigned char ICR1H_Bit0:1, ICR1H_Bit1:1, ICR1H_Bit2:1, ICR1H_Bit3:1, ICR1H_Bit4:1, ICR1H_Bit5:1, ICR1H_Bit6:1, ICR1H_Bit7:1; }; } @ 0x26;      
__io union { unsigned short OCR1B; struct { unsigned char OCR1B_Bit0:1, OCR1B_Bit1:1, OCR1B_Bit2:1, OCR1B_Bit3:1, OCR1B_Bit4:1, OCR1B_Bit5:1, OCR1B_Bit6:1, OCR1B_Bit7:1; unsigned char OCR1B_Bit8:1, OCR1B_Bit9:1, OCR1B_Bit10:1, OCR1B_Bit11:1, OCR1B_Bit12:1, OCR1B_Bit13:1, OCR1B_Bit14:1, OCR1B_Bit15:1; }; struct { unsigned char OCR1BL; unsigned char OCR1BH; }; struct { unsigned char OCR1BL_Bit0:1, OCR1BL_Bit1:1, OCR1BL_Bit2:1, OCR1BL_Bit3:1, OCR1BL_Bit4:1, OCR1BL_Bit5:1, OCR1BL_Bit6:1, OCR1BL_Bit7:1; unsigned char OCR1BH_Bit0:1, OCR1BH_Bit1:1, OCR1BH_Bit2:1, OCR1BH_Bit3:1, OCR1BH_Bit4:1, OCR1BH_Bit5:1, OCR1BH_Bit6:1, OCR1BH_Bit7:1; }; } @ 0x28;      
__io union { unsigned short OCR1A; struct { unsigned char OCR1A_Bit0:1, OCR1A_Bit1:1, OCR1A_Bit2:1, OCR1A_Bit3:1, OCR1A_Bit4:1, OCR1A_Bit5:1, OCR1A_Bit6:1, OCR1A_Bit7:1; unsigned char OCR1A_Bit8:1, OCR1A_Bit9:1, OCR1A_Bit10:1, OCR1A_Bit11:1, OCR1A_Bit12:1, OCR1A_Bit13:1, OCR1A_Bit14:1, OCR1A_Bit15:1; }; struct { unsigned char OCR1AL; unsigned char OCR1AH; }; struct { unsigned char OCR1AL_Bit0:1, OCR1AL_Bit1:1, OCR1AL_Bit2:1, OCR1AL_Bit3:1, OCR1AL_Bit4:1, OCR1AL_Bit5:1, OCR1AL_Bit6:1, OCR1AL_Bit7:1; unsigned char OCR1AH_Bit0:1, OCR1AH_Bit1:1, OCR1AH_Bit2:1, OCR1AH_Bit3:1, OCR1AH_Bit4:1, OCR1AH_Bit5:1, OCR1AH_Bit6:1, OCR1AH_Bit7:1; }; } @ 0x2A;      
__io union { unsigned short TCNT1; struct { unsigned char TCNT1_Bit0:1, TCNT1_Bit1:1, TCNT1_Bit2:1, TCNT1_Bit3:1, TCNT1_Bit4:1, TCNT1_Bit5:1, TCNT1_Bit6:1, TCNT1_Bit7:1; unsigned char TCNT1_Bit8:1, TCNT1_Bit9:1, TCNT1_Bit10:1, TCNT1_Bit11:1, TCNT1_Bit12:1, TCNT1_Bit13:1, TCNT1_Bit14:1, TCNT1_Bit15:1; }; struct { unsigned char TCNT1L; unsigned char TCNT1H; }; struct { unsigned char TCNT1L_Bit0:1, TCNT1L_Bit1:1, TCNT1L_Bit2:1, TCNT1L_Bit3:1, TCNT1L_Bit4:1, TCNT1L_Bit5:1, TCNT1L_Bit6:1, TCNT1L_Bit7:1; unsigned char TCNT1H_Bit0:1, TCNT1H_Bit1:1, TCNT1H_Bit2:1, TCNT1H_Bit3:1, TCNT1H_Bit4:1, TCNT1H_Bit5:1, TCNT1H_Bit6:1, TCNT1H_Bit7:1; }; } @ 0x2C;      
__io union { unsigned char TCCR1B; struct { unsigned char TCCR1B_Bit0:1, TCCR1B_Bit1:1, TCCR1B_Bit2:1, TCCR1B_Bit3:1, TCCR1B_Bit4:1, TCCR1B_Bit5:1, TCCR1B_Bit6:1, TCCR1B_Bit7:1; }; } @ 0x2E;      
__io union { unsigned char TCCR1A; struct { unsigned char TCCR1A_Bit0:1, TCCR1A_Bit1:1, TCCR1A_Bit2:1, TCCR1A_Bit3:1, TCCR1A_Bit4:1, TCCR1A_Bit5:1, TCCR1A_Bit6:1, TCCR1A_Bit7:1; }; } @ 0x2F;      
__io union { unsigned char SFIOR; struct { unsigned char SFIOR_Bit0:1, SFIOR_Bit1:1, SFIOR_Bit2:1, SFIOR_Bit3:1, SFIOR_Bit4:1, SFIOR_Bit5:1, SFIOR_Bit6:1, SFIOR_Bit7:1; }; } @ 0x30;      
__io union { unsigned char OSCCAL; unsigned char OCRD; struct { unsigned char OSCCAL_Bit0:1, OSCCAL_Bit1:1, OSCCAL_Bit2:1, OSCCAL_Bit3:1, OSCCAL_Bit4:1, OSCCAL_Bit5:1, OSCCAL_Bit6:1, OSCCAL_Bit7:1; }; struct { unsigned char OCRD_Bit0:1, OCRD_Bit1:1, OCRD_Bit2:1, OCRD_Bit3:1, OCRD_Bit4:1, OCRD_Bit5:1, OCRD_Bit6:1, OCRD_Bit7:1; }; } @ 0x31;
 
 
__io union { unsigned char TCNT0; struct { unsigned char TCNT0_Bit0:1, TCNT0_Bit1:1, TCNT0_Bit2:1, TCNT0_Bit3:1, TCNT0_Bit4:1, TCNT0_Bit5:1, TCNT0_Bit6:1, TCNT0_Bit7:1; }; } @ 0x32;      
__io union { unsigned char TCCR0; struct { unsigned char TCCR0_Bit0:1, TCCR0_Bit1:1, TCCR0_Bit2:1, TCCR0_Bit3:1, TCCR0_Bit4:1, TCCR0_Bit5:1, TCCR0_Bit6:1, TCCR0_Bit7:1; }; } @ 0x33;      
__io union { unsigned char MCUCSR; struct { unsigned char MCUCSR_Bit0:1, MCUCSR_Bit1:1, MCUCSR_Bit2:1, MCUCSR_Bit3:1, MCUCSR_Bit4:1, MCUCSR_Bit5:1, MCUCSR_Bit6:1, MCUCSR_Bit7:1; }; } @ 0x34;      
__io union { unsigned char MCUCR; struct { unsigned char MCUCR_Bit0:1, MCUCR_Bit1:1, MCUCR_Bit2:1, MCUCR_Bit3:1, MCUCR_Bit4:1, MCUCR_Bit5:1, MCUCR_Bit6:1, MCUCR_Bit7:1; }; } @ 0x35;      
__io union { unsigned char TWCR; struct { unsigned char TWCR_Bit0:1, TWCR_Bit1:1, TWCR_Bit2:1, TWCR_Bit3:1, TWCR_Bit4:1, TWCR_Bit5:1, TWCR_Bit6:1, TWCR_Bit7:1; }; } @ 0x36;      
__io union { unsigned char SPMCR; struct { unsigned char SPMCR_Bit0:1, SPMCR_Bit1:1, SPMCR_Bit2:1, SPMCR_Bit3:1, SPMCR_Bit4:1, SPMCR_Bit5:1, SPMCR_Bit6:1, SPMCR_Bit7:1; }; } @ 0x37;      
__io union { unsigned char TIFR; struct { unsigned char TIFR_Bit0:1, TIFR_Bit1:1, TIFR_Bit2:1, TIFR_Bit3:1, TIFR_Bit4:1, TIFR_Bit5:1, TIFR_Bit6:1, TIFR_Bit7:1; }; } @ 0x38;      
__io union { unsigned char TIMSK; struct { unsigned char TIMSK_Bit0:1, TIMSK_Bit1:1, TIMSK_Bit2:1, TIMSK_Bit3:1, TIMSK_Bit4:1, TIMSK_Bit5:1, TIMSK_Bit6:1, TIMSK_Bit7:1; }; } @ 0x39;      
__io union { unsigned char GIFR; struct { unsigned char GIFR_Bit0:1, GIFR_Bit1:1, GIFR_Bit2:1, GIFR_Bit3:1, GIFR_Bit4:1, GIFR_Bit5:1, GIFR_Bit6:1, GIFR_Bit7:1; }; } @ 0x3A;      
__io union { unsigned char GICR; struct { unsigned char GICR_Bit0:1, GICR_Bit1:1, GICR_Bit2:1, GICR_Bit3:1, GICR_Bit4:1, GICR_Bit5:1, GICR_Bit6:1, GICR_Bit7:1; }; } @ 0x3B;      
__io union { unsigned char OCR0; struct { unsigned char OCR0_Bit0:1, OCR0_Bit1:1, OCR0_Bit2:1, OCR0_Bit3:1, OCR0_Bit4:1, OCR0_Bit5:1, OCR0_Bit6:1, OCR0_Bit7:1; }; } @ 0x3C;      
__io union { unsigned short SP; struct { unsigned char SP_Bit0:1, SP_Bit1:1, SP_Bit2:1, SP_Bit3:1, SP_Bit4:1, SP_Bit5:1, SP_Bit6:1, SP_Bit7:1; unsigned char SP_Bit8:1, SP_Bit9:1, SP_Bit10:1, SP_Bit11:1, SP_Bit12:1, SP_Bit13:1, SP_Bit14:1, SP_Bit15:1; }; struct { unsigned char SPL; unsigned char SPH; }; struct { unsigned char SPL_Bit0:1, SPL_Bit1:1, SPL_Bit2:1, SPL_Bit3:1, SPL_Bit4:1, SPL_Bit5:1, SPL_Bit6:1, SPL_Bit7:1; unsigned char SPH_Bit0:1, SPH_Bit1:1, SPH_Bit2:1, SPH_Bit3:1, SPH_Bit4:1, SPH_Bit5:1, SPH_Bit6:1, SPH_Bit7:1; }; } @ 0x3D;      
__io union { unsigned char SREG; struct { unsigned char SREG_Bit0:1, SREG_Bit1:1, SREG_Bit2:1, SREG_Bit3:1, SREG_Bit4:1, SREG_Bit5:1, SREG_Bit6:1, SREG_Bit7:1; }; } @ 0x3F;      



 
 
 

 
 
 

 







 
 
 
 
 

 

 
 
 
 
 
 

 			

 

 

 
 
 
  
 		

 
 
 
 
 
 
 
                        
 
 
 

 
 
 
 
 
 
 
 
 
 
 
 
  
 
 
 
 
 
 
 
   
 
 
 
 
 
 
 
   
 
 
 
 
  
 

 

 
 
 
 
 
 
 
 
 
 
 


 
 
 

 













 



__intrinsic void __no_operation(void);
__intrinsic void __enable_interrupt(void);
__intrinsic void __disable_interrupt(void);
__intrinsic void __sleep(void);
__intrinsic void __watchdog_reset(void);

#pragma language=extended
__intrinsic unsigned char __load_program_memory(const unsigned char __flash *);
#pragma language=default

__intrinsic void __insert_opcode(unsigned short op);


#pragma language=extended
__intrinsic unsigned int  __segment_begin(const char  *segment_name);
__intrinsic unsigned int  __segment_end  (const char  *segment_name);

#pragma language=default

__intrinsic void __require(void *);

__intrinsic void __delay_cycles(unsigned long);

__intrinsic unsigned char __save_interrupt(void);
__intrinsic void          __restore_interrupt(unsigned char);

__intrinsic unsigned char __swap_nibbles(unsigned char);

__intrinsic void          __indirect_jump_to(unsigned long);


__intrinsic unsigned int  __multiply_unsigned(unsigned char, unsigned char);
__intrinsic signed int    __multiply_signed(signed char, signed char);
__intrinsic signed int    __multiply_signed_with_unsigned(signed char, unsigned char);

__intrinsic unsigned int  __fractional_multiply_unsigned(unsigned char, unsigned char);
__intrinsic signed int    __fractional_multiply_signed(signed char, signed char);
__intrinsic signed int    __fractional_multiply_signed_with_unsigned(signed char, signed char);

#pragma language=extended

 
__intrinsic void __DataToR0ByteToSPMCR_SPM(unsigned char data, 
                                           unsigned char byte);
__intrinsic void __AddrToZByteToSPMCR_SPM(void __flash* addr, 
                                          unsigned char byte);
__intrinsic void __AddrToZWordToR1R0ByteToSPMCR_SPM(void __flash* addr, 
                                                    unsigned short word, 
                                                    unsigned char byte);






__intrinsic unsigned char __AddrToZByteToSPMCR_LPM(void __flash* addr, 
                                                   unsigned char byte);




#pragma language=default



 

 














#pragma once



typedef unsigned char														bool;


typedef void(*EventHandler)(void);

typedef unsigned char														uint8;
typedef signed char															sint8;
typedef unsigned short													uint16;
typedef signed short														sint16;
typedef unsigned long														uint32;
typedef signed long															sint32;




extern void UART_Init(unsigned short baudrate);
extern void UART_SetBaudrate(unsigned short baudrate);
extern unsigned char UART_BytesReady(void);
extern unsigned char UART_ReadByte(void);
extern void UART_ReadBytes(unsigned char *buffer, unsigned char length);
extern void UART_WriteByte(unsigned char data);
extern void UART_WriteBytes(unsigned char *buffer, unsigned char length);

extern void putc(unsigned char Data);
extern int putchar(int data);
extern unsigned char getc(void);

extern void UART_WriteString(char* str);
extern void UART_WriteString_P(const char __flash* str);


extern void UART_WriteValueUnsignedChar(unsigned char value);
extern void UART_WritePointer(void* pointer);





 





static void WriteHexDigit(unsigned char value);




 
void UART_Init(unsigned short baudrate)
{
	UBRRH=(baudrate>>8);
	UBRRL=(baudrate&0xff);
	UCSRA=(1<<1);


	UCSRB=(1<<4)|(1<<3);


}




 
void UART_SetBaudrate(unsigned short baudrate)
{
	UBRRH=(baudrate>>8);
	UBRRL=(baudrate&0xff);
}



 
unsigned char UART_BytesReady(void)
{

	return ((UCSRA&(1<<7))!=0);

}




 
unsigned char UART_ReadByte(void)
{

	while(!(UCSRA&(1<<7)));
	return UDR;

}




 
void UART_ReadBytes(unsigned char *buffer, unsigned char length)
{
	while(length)
	{
		*buffer++=UART_ReadByte();
		length--;
	}
}




 
void UART_WriteByte(unsigned char data)
{

	while(!(UCSRA&(1<<5)));
	UDR=data;

}




 
void putc(unsigned char data)
{
	UART_WriteByte(data);
}
int putchar(int data)
{
	UART_WriteByte(data);
	return 0;
}




 
unsigned char getc(void)
{
	return UART_ReadByte();
}




 
void UART_WriteBytes(unsigned char *buffer, unsigned char length)
{
	while(length)
	{
		length--;
		UART_WriteByte(*buffer++);
	}
}




 




 





 
void UART_WriteString(char* str)
{
unsigned char temp;

	while(*str)
	{
		temp=*str++;
		UART_WriteByte(temp);
		if(temp==10)
			UART_WriteByte(13);
	}
}




 
void UART_WriteString_P(const char __flash* str)
{
unsigned char temp;

	while(*str)
	{
		temp=*str++;
		UART_WriteByte(temp);
		if(temp==10)
			UART_WriteByte(13);
	}
}





 
void UART_WriteValueUnsignedChar(unsigned char value)
{
	if(value > 99)
		UART_WriteByte((value / 100) + '0');
	else
		UART_WriteByte(' ');
	
	if(value > 9)
		UART_WriteByte(((value / 10) % 10) + '0');
	else
		UART_WriteByte(' ');
	
	UART_WriteByte((value % 10) + '0');
}


void UART_WritePointer(void* pointer)
{
	WriteHexDigit((unsigned short)pointer >> 12);
	WriteHexDigit((unsigned short)pointer >> 8);
	WriteHexDigit((unsigned short)pointer >> 4);
	WriteHexDigit((unsigned short)pointer);
}


static void WriteHexDigit(unsigned char value)
{
	value &= 0x0f;
	
	if(value > 9)
		UART_WriteByte(value + 'A' - 10);
	else
		UART_WriteByte(value + '0');
}



 













 



 






 
