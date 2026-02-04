using System.Text.RegularExpressions;

namespace Application.Validators
{
    public delegate bool RequiredValidatorDelegate(string fieldValue);
    public delegate bool StringLengthValidatorDelegate(string fieldValue, int minimumLength, int maximumLength);
    public delegate bool DateValidatorDelegate(string fieldValue, out DateTime validDate);
    public delegate bool PatternMatchValidatorDelegate(string fieldValue, string pattern);
    public delegate bool CompareFieldsValidatorDelegate(string fieldValue, string fieldValueToCompare);

    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidatorDelegate _requiredValidatorDelegate = null;
        private static StringLengthValidatorDelegate _stringLengthValidatorDelegate = null;
        private static DateValidatorDelegate _dateValidatorDelegate = null;
        private static PatternMatchValidatorDelegate _patternMatchValidatorDelegate = null;
        private static CompareFieldsValidatorDelegate _compareFieldsValidatorDelegate = null;

        public static RequiredValidatorDelegate RequiredValidatorDelegate
        {
            get
            {
                if (_requiredValidatorDelegate == null)
                    _requiredValidatorDelegate = new RequiredValidatorDelegate(IsRequiredFieldValid);

                return _requiredValidatorDelegate;
            }
        }

        public static StringLengthValidatorDelegate StringLengthValidatorDelegate
        {
            get
            {
                if (_stringLengthValidatorDelegate == null)
                    _stringLengthValidatorDelegate = new StringLengthValidatorDelegate(IsStringFieldLengthValid);

                return _stringLengthValidatorDelegate;
            }
        }

        public static DateValidatorDelegate DateValidatorDelegate
        {
            get
            {
                if (_dateValidatorDelegate == null)
                    _dateValidatorDelegate = new DateValidatorDelegate(IsDateFieldValid);

                return _dateValidatorDelegate;
            }
        }

        public PatternMatchValidatorDelegate PatternMatchValidatorDelegate
        {
            get
            {
                if (_patternMatchValidatorDelegate == null)
                    _patternMatchValidatorDelegate = new PatternMatchValidatorDelegate(IsFieldPatternValid);

                return _patternMatchValidatorDelegate;
            }
        }

        public CompareFieldsValidatorDelegate CompareFieldsValidatorDelegate
        {
            get
            {
                if (_compareFieldsValidatorDelegate == null)
                    _compareFieldsValidatorDelegate = new CompareFieldsValidatorDelegate(IsFieldComparisonValid);

                return _compareFieldsValidatorDelegate;
            }
        }

        private static bool IsRequiredFieldValid(string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
                return true;

            return false;
        }

        private static bool IsStringFieldLengthValid(string fieldValue, int minimumLength, int maximumLength)
        {
            if (fieldValue.Length >= minimumLength && fieldValue.Length <= maximumLength)
                return true;

            return false;
        }

        private static bool IsDateFieldValid(string dateTime, out DateTime validDateTime)
        {
            if (DateTime.TryParse(dateTime, out validDateTime))
                return true;

            return false;
        }

        private static bool IsFieldPatternValid(string fieldValue, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);

            if (regex.IsMatch(fieldValue))
                return true;

            return false;
        }

        private static bool IsFieldComparisonValid(string firstField, string secondField)
        {
            if (firstField.Equals(secondField))
                return true;

            return false;
        }
    }
}
