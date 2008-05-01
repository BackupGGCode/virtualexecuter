extern bool Kernel_CreateTask(task t);
extern bool Kernel_DeleteTask(task t);
extern void Kernel_Delete(void);

extern bool Kernel_SuspendTask(task t);
extern void Kernel_Suspend(void);
extern bool Kernel_ResumeTask(task t);

extern bool Kernel_SleepTask(task t, unsigned short time);
extern void Kernel_Sleep(unsigned short time);
extern bool Kernel_WakeTask(task t);
