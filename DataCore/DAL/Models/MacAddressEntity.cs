// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.DAL.Models
{
    public class MacAddressEntity
    {
        #region Public and private fields and properties

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (value == null || string.IsNullOrEmpty(value))
                {
                    _value = string.Empty;
                }
                else
                {
                    _value = value.Length switch
                    {
                        // 000000000000
                        12 => value,
                        // 00-00-00-00-00-00 // 00:00:00:00:00:00
                        17 => $"{value[0]}{value[1]}{value[3]}{value[4]}{value[6]}{value[7]}" +
                              $"{value[9]}{value[10]}{value[12]}{value[13]}{value[15]}{value[16]}",
                        _ => value,
                    };
                }
            }
        }

        public string ValuePrettyLookSpace => GetValueAsString(' ');

        public string ValuePrettyLookMinus => GetValueAsString('-');

        public string ValuePrettyLookColon => GetValueAsString(':');

        #endregion

        #region Constructor and destructor

        public MacAddressEntity()
        {
            _value = string.Empty;
        }

        public MacAddressEntity(string address)
        {
            _value = address;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Value)}: {ValuePrettyLookMinus}.";
        }

        public virtual bool Equals(MacAddressEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                Equals(Value, item.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MacAddressEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool EqualsNew()
        {
            return Equals(new MacAddressEntity());
        }

        public bool EqualsDefault()
        {
            return Equals(Value, string.Empty);
        }

        public object Clone()
        {
            return new MacAddressEntity
            {
                Value = Value,
            };
        }

        public void Default()
        {
            Value = "000000000000";
        }

        private string GetValueAsString(char ch)
        {
            if (Value == null || string.IsNullOrEmpty(Value))
                return string.Empty;
            return Value.Length switch
            {
                12 => $"{Value[0]}{Value[1]}{ch}{Value[2]}{Value[3]}{ch}{Value[4]}{Value[5]}{ch}" +
                      $"{Value[6]}{Value[7]}{ch}{Value[8]}{Value[9]}{ch}{Value[10]}{Value[11]}",
                17 => $"{Value[0]}{Value[1]}{ch}{Value[3]}{Value[4]}{ch}{Value[6]}{Value[7]}{ch}" +
                      $"{Value[9]}{Value[10]}{ch}{Value[12]}{Value[13]}{ch}{Value[15]}{Value[16]}",
                _ => Value,
            };
        }

        #endregion
    }
}
