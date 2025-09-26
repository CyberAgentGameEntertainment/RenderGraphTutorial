using UnityEngine.Rendering.Universal;

namespace Demo_02
{
    public class TileBasedDeferredRenderingFeature : ScriptableRendererFeature
    {
        private TileBasedDeferredRenderingPass _pass;
        public override void Create()
        {
            _pass = new()
            {
                renderPassEvent = RenderPassEvent.BeforeRenderingOpaques,
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
