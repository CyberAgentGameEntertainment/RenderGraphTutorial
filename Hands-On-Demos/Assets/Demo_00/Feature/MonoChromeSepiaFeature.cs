using UnityEngine.Rendering.Universal;

namespace Demo_00
{
    public class MonoChromeSepiaFeature : ScriptableRendererFeature
    {
        private MonoChromeSepiaPass _pass;
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
