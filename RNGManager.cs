using System;
using System.Collections.Generic;
using System.Linq;

namespace RNGManager
{
    public class RNGManager
    {
        private RNGManager()
        {
            rngInstances = new Dictionary<string, RNGInstance>();
        }

        private static RNGManager manager;
        public static RNGManager Manager
        {
            get
            {
                if (manager == null)
                    manager = new RNGManager();
                return manager;
            }
        }

        private Dictionary<string, RNGInstance> rngInstances;
        /// <summary>
        /// Get an RNG instance by it's title. If there is no instance with this title, create one.
        /// </summary>
        /// <param name="title">Title of the instance.</param>
        /// <returns></returns>
        public RNGInstance this[string title]
        {
            get
            {
                if (!rngInstances.ContainsKey(title))
                    rngInstances.Add(title, new RNGInstance(title: title));

                return rngInstances[title];
            }
        }

        /// <summary>
        /// Get an RNG instance by it's seed. If there is no instance with this seed, create one.
        /// </summary>
        /// <param name="seed">The seed of the instance.</param>
        /// <returns></returns>
        public RNGInstance this[int seed]
        {
            get
            {
                var rngInstance = rngInstances.Values.Where(x => x.Seed == seed).FirstOrDefault();

                if (rngInstance == null)
                    rngInstance = AddInstance(new RNGInstance(seed));

                return rngInstance;
            }
        }

        /// <summary>
        /// Add a new instance to the RNG manager.
        /// </summary>
        /// <param name="instance">The RNG instance to be added.</param>
        public RNGInstance AddInstance(RNGInstance instance)
        {
            if(!rngInstances.ContainsKey(instance.Title))
                rngInstances.Add(instance.Title, instance);
            
            return this[instance.Title];
        }
    }
}
