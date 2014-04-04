using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvMapper.Core.Interfaces;
using CsvMapper.Infrastructure.Actions;

namespace CsvMapper.Infrastructure.Factory
{
    public class AutoFactory
    {
        Dictionary<string, Type> _actions;

        public AutoFactory()
        {
            LoadTypesICanReturn();
        }

        public IActionable CreateInstance(string actionName)
        {
            Type t = GetTypeToCreate(actionName);

            if (t == null)
                return new NullAction();

            return Activator.CreateInstance(t) as IActionable;
        }

        Type GetTypeToCreate(string carName)
        {
            foreach (var action in _actions)
            {
                if (action.Key.Contains(carName))
                {
                    return _actions[action.Key];
                }
            }

            return null;
        }

        void LoadTypesICanReturn()
        {
            _actions = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(IActionable).ToString()) != null)
                {
                    _actions.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
}
