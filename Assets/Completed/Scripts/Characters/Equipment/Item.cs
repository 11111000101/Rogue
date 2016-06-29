
using UnityEngine;

namespace Completed
{
    public interface Item
    {
        Texture2D getIcon();
        string getIconPath();
        string getItemType();
    }
}
