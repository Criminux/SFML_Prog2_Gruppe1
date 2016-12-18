using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1.Util
{
    class MathUtil
    {
        /// <summary>
        /// Clamps the minimum and maximum of a comparable variable and returns equivalently.
        /// </summary>
        /// <typeparam name="T">
        /// Stands for everything which can be compared.
        /// </typeparam>
        /// <param name="val">
        /// Is the value of the comparable object.
        /// </param>
        /// <param name="min">
        /// Stands for the minimum which the variable can have.
        /// </param>
        /// <param name="max">
        /// Stands for the maximum which the variable can have.
        /// </param>
        /// <returns>Clamped value between minimum and maximum.</returns>
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
