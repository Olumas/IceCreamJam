using Nez;
using Nez.Tiled;

namespace IceCreamJam.Source.Tiled {
    class TiledMap : Entity {

        TmxMap map;

        public TiledMap(TmxMap map) {
            this.map = map;
        }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            AddComponent(new TiledMapRenderer(map) {RenderLayer = Constants.Layer_Map });
        }
    }
}
