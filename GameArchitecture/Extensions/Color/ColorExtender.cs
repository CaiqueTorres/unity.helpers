using UnityEngine;

namespace homehelp.Extenders
{
    public static class ColorExtender
    {
        /// <summary>
        /// Static method that extends the color methods
        /// </summary>
        /// <param name="color">Struct that will be extended</param>
        /// <returns>A new transparent color</returns>
        public static Color Transparent(this Color color)
        {
            return new Color(1, 1, 1, 0);
        }
    }
}
