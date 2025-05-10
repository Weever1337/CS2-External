using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CS2External
{
    public class Overlay : ClickableTransparentOverlay.Overlay, IOverlay
    {
        private readonly IGameSettings _settings;
        private IntPtr _cs2WindowHandle;
        private IntPtr _overlayWindowHandle;

        public Overlay(IGameSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected override void Render()
        {
            ImGui.Begin("CS2 External by Weever");
            bool isBunnyHopEnabled = _settings.IsBunnyHopEnabled;
            if (ImGui.Checkbox("Auto Bunny Hop", ref isBunnyHopEnabled))
            {
                _settings.IsBunnyHopEnabled = isBunnyHopEnabled;
            }
            bool isAntiFlashEnabled = _settings.IsAntiFlashEnabled;
            if (ImGui.Checkbox("Anti Flash", ref isAntiFlashEnabled))
            {
                _settings.IsAntiFlashEnabled = isAntiFlashEnabled;
            }
            int fov = _settings.Fov;
            ImGui.Text("Fov Changer");
            if (ImGui.SliderInt("FOV", ref fov, 58, 140))
            {
                _settings.Fov = fov;
            }
            ImGui.End();
        }
    }

    public interface IOverlay
    {
        Task Start();
    }
}