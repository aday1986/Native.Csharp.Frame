using Leo.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leo.Native.Commands
{

    public class CommandService : ICommandService
    {
      
        private readonly IRepository<TaskAuthority> repository;
        private readonly ITaskCollection tasks;

        public CommandService(IRepository<TaskAuthority> repository,ITaskCollection tasks)
        {
            this.repository = repository;
            this.tasks = tasks;
        }

        public bool AddTaskAuthority(TaskAuthority  taskAuthority)
        {
            repository.Add(taskAuthority);
            return repository.SaveChanges() > 0;
        }

        public bool HasAuthority(Command command)
        {
            var conditions = new List<Condition>();
            conditions.Add(new Condition() { Key = nameof(TaskAuthority.GroupId), Value = command.GroupId, ConditionType = ConditionEnum.Equal });
            conditions.Add(new Condition() { Key = nameof(TaskAuthority.QQId), Value = command.QQId, ConditionType = ConditionEnum.Equal });
            conditions.Add(new Condition() { Key = nameof(TaskAuthority.TaskName), Value = command.TaskName, ConditionType = ConditionEnum.Equal });
            return repository.Query(conditions).Any();
        }

        public IEnumerable<TaskAuthority> GetAllAuthority()
        {
            return repository.Query(null);
        }

        public IEnumerable<TaskAuthority> GetTaskAuthorities(long groupId, long qqId)
        {
            var conditions = new List<Condition>();
            conditions.Add(new Condition() { Key = nameof(TaskAuthority.GroupId), Value = groupId, ConditionType = ConditionEnum.Equal });
            conditions.Add(new Condition() { Key = nameof(TaskAuthority.QQId), Value = qqId, ConditionType = ConditionEnum.Equal });
            return repository.Query(conditions);
        }

        public bool RemoveTaskAuthority(TaskAuthority  taskAuthority)
        {
            repository.Remove(taskAuthority);
            return repository.SaveChanges() > 0;
        }



        public bool Execute(Command command, out string message)
        {
            bool result = false;
            if (tasks.TryGetValue(command.TaskName, out Task action))
            {
                if (!action.NeedValidation || HasAuthority(command))
                {
                    try
                    {
                        message = action.Func?.Invoke(command);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        result = false;
                    }
                }
                else
                {
                    message = $"您没有调用[{command.TaskName}]的权限。";
                    result = false;
                }
            }
            else
            {
                message = $"没有[{command.TaskName}]这个命令。";
            }
            return result;
        }

        public bool IsCommand(string message, out string commandName)
        {
            commandName = string.Empty;
            if (message.StartsWith(Command.Separator.ToString()))
            {
                commandName = message.Remove(0, 1).Split(Command.Separator).FirstOrDefault().Trim();
            }
            return !string.IsNullOrEmpty(commandName);
        }
    }
}
