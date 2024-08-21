namespace Architecture
{
    public sealed class SceneManagerExample : SceneManagerBase
    {
        public override void InitSceneMap()
        {
            _sceneConfigMap[SceneConfigExample.SCENE_NAME] = new SceneConfigExample();
        }
    }
}