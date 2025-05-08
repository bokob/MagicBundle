public class Define
{
    #region Scene
    // 씬 타입(이름)
    public enum SceneType
    {
        None,
        TitleScene,
        InGameScene,
        EndingScene
    }
    #endregion

    #region Sound
    // Resources 내의 BGM, SFX 폴더에 같은 이름으로 둬야함
    public enum BGM
    {
        None,
        TitleScene,
        InGameScene,
        EndingScene
    }
    public enum SFX
    {
        None,
    }
    #endregion

    public enum ElementalType
    {
        None,       // 무속성
        Fire,       // 불
        Water,      // 물
        Earth,      // 땅
        Air,        // 공기
        Explosion,  // 폭발 (불, 불)
        Flood,      // 홍수 (물, 물)
        Earthquake, // 지진 (땅, 땅)
        Storm,      // 폭풍 (공기, 공기)
        Fog,        // 안개 (불, 물)
        Lava,       // 마그마 (불, 땅)
        Lightning,  // 번개 (불, 공기)
        Plant,      // 식물 (물, 땅)
        Ice,        // 얼음 (물, 공기)
        Sand,       // 모래 (땅, 공기)
    }
}