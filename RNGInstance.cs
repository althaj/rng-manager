using System;
using System.Collections.Generic;
using System.Linq;

namespace RNGManager
{
    public class RNGInstance
    {
        private Random random;

        public string Title { get; private set; }
        public int Seed { get; private set; }

        /// <summary>
        /// Creates a new RNG instance. You can supply a seed and/or a title.
        /// </summary>
        /// <param name="seed">Seed of the instance.</param>
        /// <param name="title">Title of the instance.</param>
        public RNGInstance(int? seed = null, string title = "")
        {
            Seed = seed.HasValue ? seed.Value : Guid.NewGuid().GetHashCode();
            Title = !string.IsNullOrEmpty(title) ? title : Seed.ToString();
            random = new Random(Seed);
        }

        /// <summary>
        /// Returns a random bool. You can specify percentage chance.
        /// </summary>
        /// <param name="percentage">Chance of true result.</param>
        /// <returns></returns>
        public bool NextBool(double percentage = 0.5)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException(nameof(percentage), "Percentahe has to be between 0 and 1.");

            return percentage > random.NextDouble();
        }

        /// <summary>
        /// Returns a random float. Minimum is inclusive 0, maximum is noninclusive 1.
        /// </summary>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public float NextFloat()
        {
            return Convert.ToSingle(random.NextDouble());
        }

        /// <summary>
        /// Returns a random float. Minimum is inclusive 0, maximum is noninclusive.
        /// </summary>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public float NextFloat(float max)
        {
            return Convert.ToSingle(random.NextDouble()) * max;
        }

        /// <summary>
        /// Returns a random float. Minimum is inclusive, maximum is not.
        /// </summary>
        /// <param name="min">The returned value will be greater or equal than this minimum.</param>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public float NextFloat(float min, float max)
        {
            return Convert.ToSingle(random.NextDouble()) * (max - min) + min;
        }

        /// <summary>
        /// Returns a random integer. Minimum is inclusive 0, maximum is noninclusive.
        /// </summary>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public int NextInt(int max)
        {
            return random.Next(max);
        }

        /// <summary>
        /// Returns a random integer. Minimum is inclusive, maximum is noninclusive.
        /// </summary>
        /// /// <param name="min">The returned value will be greater or equal than this minimum.</param>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public int NextInt(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Returns a random value from an enum.
        /// </summary>
        /// <typeparam name="T">Enum containing the random value from.</typeparam>
        /// <returns></returns>
        public T NextEnumValue<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }

        /// <summary>
        /// Returns a random element from a collection.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="elements">The source collection.</param>
        /// <returns></returns>
        public T NextElement<T>(IEnumerable<T> elements)
        {
            return elements.ElementAt(NextInt(elements.Count()));
        }
    }
}
