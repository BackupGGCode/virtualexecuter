extern bool KernelCreateTask(task t);
extern bool KernelDeleteTask(task t);
extern void KernelDelete(void);

extern bool KernelSuspendTask(task t);
extern void KernelSuspend(void);
extern bool KernelResumeTask(task t);

extern bool KernelSleepTask(task t, unsigned short time);
extern void KernelSleep(unsigned short time);
extern bool KernelWakeTask(task t);
