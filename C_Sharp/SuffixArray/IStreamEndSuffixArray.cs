namespace SuffixArray
{
    /// <summary>
    /// Stream Suffix Array interface
    /// </summary>
    public interface IStreamEndSuffixArray : ISuffixArray
    {
        void AddCharAtEnd(char sym);
        
        void AddStringAtEnd(string str);
    }
}