using CustomStrings;
using Treap;
using Treap.Interfaces;

namespace SuffixArray.InvertedSuffixArray
{
    /// <summary>
    /// Inverted Suffix Array.
    /// </summary>
    public class InvertedSuffixArray : AbstractInvertedSuffixArray
    {
        private readonly IInverseIntTreap<int> array = new InverseIntTreap<int>();

        private readonly IBeginningStreamString str = new BeginningStreamString();

        public override IString String
        {
            get { return str; }
        }

        public override IBeginningStreamString StreamString
        {
            get { return str; }
        }

        public override IInverseIntTreap<int> Array
        {
            get { return array; }
        }
    }
}