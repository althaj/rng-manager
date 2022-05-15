using System;

namespace RNGManager
{
    public class RNGInstance
    {
        private Random random;

        public string Title { get; private set; }
        public int Seed { get; private set; }

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
        /// Returns a random float. You can specify minimum and maximum values. Minimum is inclusive, maximum is not.
        /// </summary>
        /// <param name="min">The returned value will be greater or equal than this minimum.</param>
        /// <param name="max">The returned value will be lower than this maximum.</param>
        /// <returns></returns>
        public float NextFloat(float min = 0, float max = 1f)
        {
            return Convert.ToSingle(random.NextDouble()) * (max - min) + min;
        }
    }
}
