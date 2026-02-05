namespace Application.Interfaces
{
    public delegate bool FieldValidatorDelegate(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage);

    public interface IFieldValidator
    {
        void InitialiseValidatorDelegates();
        string[] FieldArray { get; }
        FieldValidatorDelegate FieldValidatorDelegate { get; }
    }
}
