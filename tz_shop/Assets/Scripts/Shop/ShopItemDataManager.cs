using System;

[System.Serializable]
public class ShopItemDataManager
{
    public int[] shopItems;
    public int[] temporaryShopItem;
    public CustomTimerValue[] customTimerValues;

    public ShopItemDataManager()
    {
    }

    ~ShopItemDataManager()
    {
        shopItems = null;
        temporaryShopItem = null;
    }

    public bool TryGetTiemById(int id, out CustomTimerValue timer)
    {
        timer = default;
        int size = temporaryShopItem.Length;
        for (int i = 0; i < size; ++i)
            if (temporaryShopItem[i] == id)
            {
                timer = customTimerValues[i];
                return true;
            }
        return false;
    }

    public void RegisterBoughtItem(ShopItemSO item)
    {
        if (shopItems == null || !ContainsShopItem(item.gameItem.GetInstanceID()))
        {
            shopItems = AddItem(item.gameItem.GetInstanceID());
            if (item.gameItem.isTemporary)
            {
                temporaryShopItem = AddTempItem(item.gameItem.GetInstanceID());
                customTimerValues = AddTimerValue(item.gameItem.leftTime);
            }
        }
    }
    public void RemoveBoughtItem(ShopItemSO item)
    {
        if (ContainsShopItem(item.gameItem.GetInstanceID()))
        {
            shopItems = RemoveItem(item.gameItem.GetInstanceID());
            if (item.gameItem.isTemporary)
            {
                temporaryShopItem = RemoveTempItem(item.gameItem.GetInstanceID());
                customTimerValues = RemoveTimerValue(item.gameItem.leftTime);
            }
        }
    }

    public void RemoveBoughtItemById(int id)
    {
        if (ContainsShopItem(id))
            shopItems = RemoveItem(id);

        if (ContainsTempItem(id))
        {
            customTimerValues = RemoveTempTimer(id);
            temporaryShopItem = RemoveTempItem(id);
        }
    }

    private CustomTimerValue[] AddTimerValue(CustomTimerValue time)
    {
        if (customTimerValues == null)
            return new CustomTimerValue[] { time };
        int size = customTimerValues.Length;
        CustomTimerValue[] result = new CustomTimerValue[size + 1];
        for (int i = 0; i < size; ++i)
            result[i] = customTimerValues[i];
        result[size] = time;
        return result;
    }

    private CustomTimerValue[] RemoveTimerValue(CustomTimerValue time)
    {
        int size = customTimerValues == null ? 0 : customTimerValues.Length - 1;
        int prevSize = customTimerValues.Length;
        CustomTimerValue[] result = new CustomTimerValue[size + 1];
        int index = 0;
        for (int i = 0; i < prevSize; ++i)
        {
            if (customTimerValues[i].Equals(time)) continue;
            result[index] = customTimerValues[i];
            index++;
        }
        return result;
    }

    private CustomTimerValue[] RemoveTempTimer(int num)
    {
        int size = temporaryShopItem == null ? 0 : temporaryShopItem.Length - 1;
        int prevSize = temporaryShopItem.Length;
        CustomTimerValue[] result = new CustomTimerValue[size];
        int iteratorId = 0;

        for (int i = 0; i < prevSize; ++i)
        {
            if (temporaryShopItem[i] == num) continue;
            result[iteratorId] = customTimerValues[i];
            iteratorId++;
        }
        return result;
    }

    private int[] AddTempItem(int num)
    {
        if (temporaryShopItem == null) return new int[] { num };

        int size = temporaryShopItem.Length;
        int[] result = new int[size + 1];
        for (int i = 0; i < size; ++i)
            result[i] = temporaryShopItem[i];
        result[size] = num;
        return result;
    }

    private int[] RemoveTempItem(int num)
    {
        int size = temporaryShopItem == null ? 0 : temporaryShopItem.Length - 1;
        int prevSize = temporaryShopItem.Length;
        int[] result = new int[size];
        int iteratorId = 0;

        for (int i = 0; i < prevSize; ++i)
        {
            if (temporaryShopItem[i] == num) continue;
            result[iteratorId] = temporaryShopItem[i];
            iteratorId++;
        }
        return result;
    }

    private int[] AddItem(int num)
    {
        if (shopItems == null) return new int[] { num };

        int size = shopItems.Length + 1;
        int[] result = new int[size];
        for (int i = 0; i < shopItems.Length; ++i)
            result[i] = shopItems[i];
        result[size - 1] = num;
        return result;
    }

    private int[] RemoveItem(int num)
    {
        int size = shopItems == null ? 0 : shopItems.Length - 1;
        int prevSize = shopItems.Length;
        int[] result = new int[size];
        int iteratorId = 0;

        for (int i = 0; i < prevSize; ++i)
        {
            if (shopItems[i] == num) continue;
            result[iteratorId] = shopItems[i];
            iteratorId++;
        }
        return result;
    }

    public bool ContainsShopItem(int instanceID)
    {
        if (shopItems == null) return false;

        int size = shopItems.Length;
        for (int i = 0; i < size; ++i)
            if (shopItems[i] == instanceID) return true;
        return false;
    }

    public bool ContainsTempItem(int instanceID)
    {
        if (temporaryShopItem == null) return false;

        int size = temporaryShopItem.Length;
        for (int i = 0; i < size; ++i)
            if (temporaryShopItem[i] == instanceID) return true;

        return false;
    }
}

[System.Serializable]
public class CustomTimerValue
{
    public float hour;
    public float minute;
    public float second;

    public CustomTimerValue(float hour, float minute, float second)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }

    public bool IsEqualsTo(CustomTimerValue other)
    {
        return this.hour == other.hour && this.minute == other.minute && this.second == other.second;
    }

    internal string ToLongTimeString()
    {
        string hourStr = hour < 10.0f ? "0" + hour.ToString("F0") : hour.ToString("F0");
        string minStr = minute < 10.0f ? "0" + minute.ToString("F0") : minute.ToString("F0");
        string secStr = second < 10.0f ? "0" + second.ToString("F0") : second.ToString("F0");
        return hourStr + ":" + minStr + ":" + secStr;
    }

    internal bool TryAddSeconds(float sec)
    {
        second -= sec;
        if (second < 0)
        {
            second = 59;
            minute--;
            if (minute < 0)
            {
                minute = 59;
                hour--;
                if (hour < 0)
                    return false;
            }
        }
        return true;
    }
}
