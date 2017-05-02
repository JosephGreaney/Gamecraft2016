using UnityEngine;

namespace LevelLoading
{

    public class LevelLoader : MonoBehaviour
    {
        public Texture2D levelMap;
        public ColorToPrefab[] colorToPrefab;

        private void Start()
        {
            LoadMap();
        }

        private void FindMap()
        {
            levelMap = Resources.Load<Texture2D>("Maps/Level" + Globals.GetLevelToLoad());
        }
        /// <summary>
        /// Destroy all gameobjects set to child of the levelloader
        /// </summary>
        private void EmptyMap()
        {
            while(transform.childCount > 0)
            {
                Transform c = transform.GetChild(0);
                c.SetParent(null);
                Destroy(c.gameObject);
            }
        }

        private void LoadMap()
        {
            FindMap();
            if(levelMap == null)
            {
                Globals.LoadLevelSelect();
                return;
            }
            EmptyMap();

            // Get raw pixels from level texture
            Color32[] allPixels = levelMap.GetPixels32();
            int width = levelMap.width;
            int height = levelMap.height;

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    SpawnTileAt(allPixels[(y * width) + x], x, y);
                }
            }
        }

        private void SpawnTileAt(Color32 c, int x, int y)
        {
            if(c.a <= 0)
            {
                return;
            }
            foreach(ColorToPrefab ctp in colorToPrefab)
            {
                if (ctp.color.Equals(c))
                {
                    GameObject obj = (GameObject)Instantiate(ctp.prefab, new Vector3(x,y,0), Quaternion.identity);
                    obj.transform.parent = transform;
                    return;
                }
            }
            Debug.LogError("No color to prefab found for: " + c.ToString() + " x:" + x + " y:" + y );
        }
    }
}
