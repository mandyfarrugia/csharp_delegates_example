using Application.Interfaces;
using Shared.Constants;

namespace Application.Validators
{
    public class UserRegistrationValidator : IFieldValidator
    {
        const int FIRST_NAME_MINIMUM_LENGTH = 2;
        const int FIRST_NAME_MAXIMUM_LENGTH = 100;
        const int LAST_NAME_MINIMUM_LENGTH = 2;
        const int LAST_NAME_MAXIMUM_LENGTH = 100;

        delegate bool EmailExistsValidatorDelegate(string emailAddress);

        FieldValidatorDelegate _fieldValidatorDelegate = null;
        RequiredValidatorDelegate _requiredValidatorDelegate = null;
        StringLengthValidatorDelegate _stringLengthValidatorDelegate = null;
        DateValidatorDelegate _dateValidatorDelegate = null;
        PatternMatchValidatorDelegate _patternMatchValidatorDelegate = null;
        CompareFieldsValidatorDelegate _compareFieldsValidatorDelegate = null;
        EmailExistsValidatorDelegate _emailExistsValidatorDelegate = null;

        string[] _fieldArray = null;

        public string[] FieldArray
        {
            get
            {
                if(this._fieldArray == null)
                    this._fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];

                return this._fieldArray;
            }
        }

        public FieldValidatorDelegate FieldValidatorDelegate
        {
            get
            {
                return this._fieldValidatorDelegate;
            }
        }

        public UserRegistrationValidator() {}

        public void InitialiseValidatorDelegates()
        {
            this._requiredValidatorDelegate = CommonFieldValidatorFunctions.RequiredValidatorDelegate;
            this._stringLengthValidatorDelegate = CommonFieldValidatorFunctions.StringLengthValidatorDelegate;
            this._dateValidatorDelegate = CommonFieldValidatorFunctions.DateValidatorDelegate;
            this._patternMatchValidatorDelegate = CommonFieldValidatorFunctions.PatternMatchValidatorDelegate;
            this._compareFieldsValidatorDelegate = CommonFieldValidatorFunctions.CompareFieldsValidatorDelegate;
        }

        private bool ValidateField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = string.Empty;
            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch(userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._patternMatchValidatorDelegate(fieldValue, CommonRegularExpressionValidationPatterns.EMAIL_ADDRESS_REGULAR_EXPRESSION_PATTERN)) ? $"You must enter a valid email address.{Environment.NewLine}" : fieldInvalidMessage;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && this._emailExistsValidatorDelegate(fieldValue)) ? $"This email address already exists. Please try again{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._stringLengthValidatorDelegate(fieldValue, FIRST_NAME_MINIMUM_LENGTH, FIRST_NAME_MAXIMUM_LENGTH)) ? $"{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FIRST_NAME_MINIMUM_LENGTH} and {FIRST_NAME_MAXIMUM_LENGTH} characters long.{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._stringLengthValidatorDelegate(fieldValue, LAST_NAME_MINIMUM_LENGTH, LAST_NAME_MAXIMUM_LENGTH)) ? $"{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LAST_NAME_MINIMUM_LENGTH} and {LAST_NAME_MAXIMUM_LENGTH} characters long.{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._patternMatchValidatorDelegate(fieldValue, CommonRegularExpressionValidationPatterns.STRONG_PASSWORD_REGULAR_EXPRESSION_PATTERN)) ? $"Your password must contain at least 1 small-case letter, 1 capital letter, 1 special character and the length should be between 6 - 10 characters{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._compareFieldsValidatorDelegate(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"Your entry did not match your password{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._dateValidatorDelegate(fieldValue, out DateTime validDate)) ? $"You did not enter a valid date{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._patternMatchValidatorDelegate(fieldValue, CommonRegularExpressionValidationPatterns.UK_PHONE_NUMBER_REGULAR_EXPRESSION_PATTERN)) ? $"You did not enter a valid UK phone number{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMessage = (!this._requiredValidatorDelegate(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMessage = (fieldInvalidMessage.Equals(string.Empty) && !this._patternMatchValidatorDelegate(fieldValue, CommonRegularExpressionValidationPatterns.UK_POST_CODE_REGULAR_EXPRESSION_PATTERN)) ? $"You did not enter a valid UK post code{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                default:
                    throw new ArgumentException("This field does not exist!");
            }

            return fieldInvalidMessage.Equals(string.Empty);
        }
    }
}


