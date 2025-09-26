using UnityEngine.Rendering.Universal;

namespace Demo_01
{
    public class MonoChromeSepeaNoiseFeature : ScriptableRendererFeature
    {
        private MonoChromeSepeaNoisePass _pass;
        public override void Create()
        {
            _pass = new()
            {
                renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }
        
        protected override void Dispose(bool disposing)
        {
            _pass.Dispose();
        }
    }
}
