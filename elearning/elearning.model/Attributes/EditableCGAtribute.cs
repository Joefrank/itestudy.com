using System;


namespace elearning.model.Attributes
{
    public class EditableCGAttribute : Attribute
    {
        private int _editLevel;

        public int EditLevel { get { return _editLevel; } }

        public EditableCGAttribute(int editLevel)
        {
            _editLevel = editLevel;
        }
    }
}
