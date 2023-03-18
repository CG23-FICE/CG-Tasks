using MainProject.Interfaces;
using MainProject.Objects;

namespace MainProject
{
    public class Scene
    {
        public List<IIntersectable> Figures { get; } = new List<IIntersectable>();
        private Normal _lightSource;
        public Normal LightSource
        {
            get
            {
                return _lightSource;
            }
            set
            {
                _lightSource = value;
            }
        }
        public Camera Camera { get; set; }
    }
}
