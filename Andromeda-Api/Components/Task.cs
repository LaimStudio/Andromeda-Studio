using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Components
{
    public abstract class Task
    {
        private TaskStatus _status = TaskStatus.Planned;

        /// <summary>
        /// Начинает выполнение задачи
        /// </summary>
        public virtual void Run()
        {
            Status = TaskStatus.Running;
        }

        /// <summary>
        /// Статус задачи
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnStatus?.Invoke(value);
            }
        }

        /// <summary>
        /// Обработчик события изменения статуса
        /// </summary>
        /// <param name="status"></param>
        public delegate void StatusHandler(TaskStatus status);

        /// <summary>
        /// Возникает при изменении статуса
        /// </summary>
        public event StatusHandler OnStatus;

        /// <summary>
        /// Все возможные состояния статуса задачи
        /// </summary>
        public enum TaskStatus
        {
            Planned,
            Running,
            Finished,
            Error,
            Stopped,
            Cancelled
        }
    }
}
