/// <summary>
/// Class used to find the index of a certain element in a readOnlyList (e.g. useful for finding player's name in the list of users). 
/// </summary>
public static class GetIndex
{
    public static int IndexOf<T>(this System.Collections.Generic.IReadOnlyList<T> self, T elementToFind)
    {
        int i = 0;
        foreach (T element in self)
        {
            if (Equals(element, elementToFind))
                return i;
            i++;
        }
        return -1;
    }
}
