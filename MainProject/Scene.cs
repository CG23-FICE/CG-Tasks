using MainProject.Interfaces;
using MainProject.Objects;

namespace MainProject
{
    public class Scene
    {
        public List<IIntersectable> Figures { get; } = new List<IIntersectable>();
        private Vector _lightSource;
        public Vector LightSource
        {
            get
            {
                return _lightSource;
            }
            set
            {
                _lightSource = value.Normalize();
            }
        }
        public Camera Camera { get; set; }
    }
}
