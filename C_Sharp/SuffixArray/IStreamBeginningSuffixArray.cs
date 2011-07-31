namespace SuffixArray
{
    /// <summary>
    /// Stream Suffix Array interface
    /// </summary>
    public interface IStreamBeginningSuffixArray : ISuffixArray
    {
        void AddCharAtBegin(char ch);
        
        void AddStringAtBegin(string str);
    }
}