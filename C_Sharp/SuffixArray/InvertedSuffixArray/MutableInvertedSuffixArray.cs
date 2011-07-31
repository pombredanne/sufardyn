using CustomStrings;
using Treap;
using Treap.Interfaces;

namespace SuffixArray.InvertedSuffixArray
{
    public class MutableInvertedSuffixArray : AbstractInvertedSuffixArray
    {
        readonly SimpleMutableString simpleMutableString = new SimpleMutableString();

        private readonly PopedInverseIntTreap array = new PopedInverseIntTreap();

        public override IInverseIntTreap<int> Array
        {
            get { return array; }
        }

        public override IBeginningStreamString StreamString
        {
            get { return simpleMutableString; }
        }

        public void Remove(int idx)
        {
            array.Delete(idx);
            simpleMutableString.Remove(idx);
        }

        public override IString String
        {
            get { return simpleMutableString; }
        }
    }
}