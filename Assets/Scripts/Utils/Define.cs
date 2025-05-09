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

        // 조합하고 나온 결과
        Explosion,  // 폭발
        Flood,      // 홍수
        Earthquake, // 지진
        Storm,      // 폭풍
        Fog,        // 안개
        Lava,       // 마그마
        Lightning,  // 번개
        Plant,      // 식물
        Ice,        // 얼음
        Sand,       // 모래
    }

    // 원소 타입 조합
    public enum ElementalTypeCombine
    {
        Explosion  = (1<< ElementalType.Fire) | (1 << ElementalType.Fire),       // 폭발 (불, 불)
        Flood      = (1 << ElementalType.Water) | (1 << ElementalType.Water),    // 홍수 (물, 물)
        Earthquake = (1 << ElementalType.Earth) | (1 << ElementalType.Earth),    // 지진 (땅, 땅)
        Storm      = (1 << ElementalType.Air) | (1 << ElementalType.Air),        // 폭풍 (공기, 공기)
        Fog        = (1 << ElementalType.Fire) | (1 << ElementalType.Water),     // 안개 (불, 물)
        Lava       = (1 << ElementalType.Fire) | (1 << ElementalType.Earth),     // 마그마 (불, 땅)
        Lightning  = (1 << ElementalType.Fire) | (1 << ElementalType.Air),       // 번개 (불, 공기)
        Plant      = (1 << ElementalType.Water) | (1 << ElementalType.Earth),    // 식물 (물, 땅)
        Ice        = (1 << ElementalType.Water) | (1 << ElementalType.Air),      // 얼음 (물, 공기)
        Sand       = (1 << ElementalType.Earth) | (1 << ElementalType.Air),      // 모래 (땅, 공기)
    }
}