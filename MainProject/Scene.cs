﻿using MainProject.Interfaces;
using MainProject.Models.Basics;
using MainProject.Objects;

namespace MainProject
{
    public class Scene
    {
        public List<IIntersectable> Figures { get; } = new List<IIntersectable>();
        private Point _lightSource;
        public Point LightSource
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
