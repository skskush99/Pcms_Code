using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class ConversionUtility
    {
        #region Private static varialbe
        static private String[,] _arrstrMonthNames = {{"January","February","March","April","May","June","July","August","September","October","November","December"},
                                                     {"January","February","March","April","May","June","July","August","September","October","November","December"},
                                                     {"जनवरी","फ़रवरी","मार्च","अप्रैल","मई","जून","जुलाई","अगस्त","सितम्बर","अक्टूबर","नवम्बर","दिसम्बर"},
                                                     {"ਜਨਵਰੀ","ਫ਼ਰਵਰੀ","ਮਾਰਚ","ਅਪਰੈਲ","ਮਈ","ਜੂਨ","ਜੁਲਾਈ","ਅਗਸਤ","ਸਤੰਬਰ","ਅਕਤੂਬਰ","ਨਵੰਬਰ","ਦਸੰਬਰ"}
                                                     };
        static private String[,] _arrstrNumbers = {{"","One","Two","Three","Four","Five","Six","Seven","Eight","Nine",
            "Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen", "Seventeen","Eighteen","Ninteen",
            "Twenty","Twenty One","Twenty Two","Twenty Three","Twenty Four","Twenty Five","Twenty Six", "Twenty  Seven","Twenty Eight","Twenty Nine",
            "Thirty","Thirty One","Thirty Two","Thirty Three","Thirty Four","Thirty Five","Thirty Six", "Thirty  Seven","Thirty Eight","Thirty Nine",
            "Forty","Forty One","Forty Two","Forty Three","Forty Four","Forty Five","Forty Six", "Forty  Seven","Forty Eight","Forty Nine",
            "Fifty","Fifty One","Fifty Two","Fifty Three","Fifty Four","Fifty Five","Fifty Six", "Fifty  Seven","Fifty Eight","Fifty Nine",
            "Sixty","Sixty One","Sixty Two","Sixty Three","Sixty Four","Sixty Five","Sixty Six", "Sixty  Seven","Sixty Eight","Sixty Nine",
            "Seventy","Seventy One","Seventy Two","Seventy Three","Seventy Four","Seventy Five","Seventy Six", "Seventy  Seven","Seventy Eight","Seventy Nine",
            "Eighty","Eighty One","Eighty Two","Eighty Three","Eighty Four","Eighty Five","Eighty Six", "Eighty  Seven","Eighty Eight","Eighty Nine",
            "Ninety","Ninety One","Ninety Two","Ninety Three","Ninety Four","Ninety Five","Ninety Six", "Ninety  Seven","Ninety Eight","Ninety Nine"
           },
           {"","One","Two","Three","Four","Five","Six","Seven","Eight","Nine",
            "Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen", "Seventeen","Eighteen","Ninteen",
            "Twenty","Twenty One","Twenty Two","Twenty Three","Twenty Four","Twenty Five","Twenty Six", "Twenty  Seven","Twenty Eight","Twenty Nine",
            "Thirty","Thirty One","Thirty Two","Thirty Three","Thirty Four","Thirty Five","Thirty Six", "Thirty  Seven","Thirty Eight","Thirty Nine",
            "Forty","Forty One","Forty Two","Forty Three","Forty Four","Forty Five","Forty Six", "Forty  Seven","Forty Eight","Forty Nine",
            "Fifty","Fifty One","Fifty Two","Fifty Three","Fifty Four","Fifty Five","Fifty Six", "Fifty  Seven","Fifty Eight","Fifty Nine",
            "Sixty","Sixty One","Sixty Two","Sixty Three","Sixty Four","Sixty Five","Sixty Six", "Sixty  Seven","Sixty Eight","Sixty Nine",
            "Seventy","Seventy One","Seventy Two","Seventy Three","Seventy Four","Seventy Five","Seventy Six", "Seventy  Seven","Seventy Eight","Seventy Nine",
            "Eighty","Eighty One","Eighty Two","Eighty Three","Eighty Four","Eighty Five","Eighty Six", "Eighty  Seven","Eighty Eight","Eighty Nine",
            "Ninety","Ninety One","Ninety Two","Ninety Three","Ninety Four","Ninety Five","Ninety Six", "Ninety  Seven","Ninety Eight","Ninety Nine"
           },
            {"","एक","दो","तीन","चार","पाँच","छः","सात","आठ","नौ",
            "दस","ग्यारह","बारह","तेरह","चौदह","पंद्रह","सोलह", "सत्तरह","अठारह","उन्नीस",
            "बीस","इक्कीस","बाईस","तेईस","चौबीस","पच्चीस","छब्बीस", "सत्ताईस","अट्ठाईस","उनतीस",
            "तीस","इकतीस","बत्तीस","तैंतीस","चौंतीस","पैंतीस","छत्तीस", "सैंतीस","अड़तीस","उनतालीस",
            "चालीस","इकतालीस","बयालीस","तैंतालीस","चौवालीस","पैंतालीस","छियालीस", "सैंतालीस","अड़तालीस","उनचास",
            "पचास","इक्यावन","बावन","तिरेपन","चौवन","पचपन","छप्पन", "सत्तावन","अट्ठावन","उनसठ",
            "साठ","इकसठ","बासठ","तिरेसठ","चौंसठ","पैंसठ","छियासठ","सड़सठ","अड़सठ","उनहत्तर",
            "सत्तर","इकहत्तर","बहत्तर","तिहत्तर","चौहत्तर","पचहत्तर","छिहत्तर","सतहत्तर","अठहत्तर","उनासी",
            "अस्सी","इक्यासी","बयासी","तिरासी","चौरासी","पचासी","छियासी","सत्तासी","अट्ठासी","नवासी",
            "नब्बे","इक्यानबे","बानबे","तिरानबे","चौरानबे","पंचानबे","छियानबे","सत्तानबे","अट्ठानबे","निन्यानबे"
           },
            {"","ਇਕ","ਦੋ","ਤਿੰਨ","ਚਾਰ","ਪੰਜ","ਛੇ","ਸੱਤ","ਅੱਠ","ਨੌਂ",
            "ਦੱਸ","ਗਿਆਰਾਂ","ਬਾਰਾਂ","ਤੇਰਾਂ","ਚੌਦਾਂ","ਪੰਦਰਾਂ","ਸੋਲ਼ਾਂ", "ਸਤਾਰਾਂ","ਅਠਾਰਾਂ","ਉਨੀ",
            "ਵੀਹ","ਇੱਕੀ","ਬਾਈ","ਤੇਈ","ਚੌਵੀ","ਪੰਝੀ","ਛੱਬੀ", "ਸਤਾਈ","ਅਠਾਈ","ਉਨੱਤੀ",
            "ਤੀਹ","ਇਕੱਤੀ","ਬੱਤੀ","ਤੇਤੀ","ਚੌਂਤੀ","ਪੈਂਤੀ","ਛੱਤੀ", "ਸੈਂਤੀ","ਅਠੱਤੀ","ਉਨਤਾਲੀ",
            "ਚਾਲੀ","ਇਕਤਾਲੀ","ਬਤਾਲੀ","ਤਰਤਾਲੀ","ਚੌਤਾਲੀ","ਪੰਜਤਾਲੀ","ਛਿਆਲੀ", "ਸੰਤਾਲੀ","ਅੱਠਤਾਲੀ","ਉਣਿੰਜਾ",
            "ਪੰਜਾਹ","ਇਕਵਿੰਜਾ","ਬਵਿੰਜਾ","ਤਰਵਿੰਜਾ","ਚਰਿੰਜਾ","ਪਚਵਿੰਜਾ","ਛਪਿੰਜਾ", "ਸਤਵਿੰਜਾ","ਅੱਠਵਿੰਜਾ","ਉਣਾਠ",
            "ਸੱਠ","ਇਕਾਠ","ਬਾਠ੍ਹ","ਤਰੇਠ੍ਹ","ਚੌਠ੍ਹ","ਪੈਂਠ","ਛਿਆਠ","ਸਤਾਹਠ","ਅੱਠਾਠ","ਉਣੱਤਰ",
            "ਸੱਤਰ","ਇਕ੍ਹੱਤਰ","ਬਹੱਤਰ","ਤਹੱਤਰ","ਚੌਹੱਤਰ","ਪੰਜੱਤਰ","ਛਿਹੱਤਰ","ਸਤੱਤਰ","ਅਠੱਤਰ","ਉਣਾਸੀ",
            "ਅੱਸੀ","ਇਕਾਸੀ","ਬਿਆਸੀ","ਤਰਆਸੀ","ਚਰਾਸੀ","ਪੰਜਾਸੀ","ਛਿਆਸੀ","ਸਤਾਸੀ","ਅਠਾਸੀ","ਉਣਾਂਨਵੇਂ",
            "ਨੱਬੇ","ਇਕਆਨਵੇਂ","ਬਿਆਨਵੇਂ","ਤਰਆਨਵੇਂ","ਚਰਾਨਵੇਂ","ਪਚਾਨਵੇਂ","ਛਿਆਨਵੇਂ","ਸਤਾਨਵੇਂ","ਅਠਾਨਵੇਂ","ਨਿੜੱਨਵੇਂ"
           }
          };
        static private String[] _arrstrCrore = { "Crore", "Crore", "करोड़", "ਕਰੋੜ " };
        static private String[] _arrstrLac = { "Lakh", "Lakh", "लाख", "ਲੱਖ " };
        static private String[] _arrstrThousand = { "Thousand", "Thousand", "हजार", "ਹਜਾਰ" };
        static private String[] _arrstrHundred = { "Hundred", "Hundred", "सौ", "ਸੌ" };
        static private String[] _arrstrRupee = { "Rupees", "Rupees", "रुपये", "ਰੁਪਏ" };
        static private String[] _arrstrDolar = { "Dollars", "Dollars", "डॉलर", "ਡਾਲਰ" };
        static private String[] _arrstrRupeeOnly = { "Rupees", "Rupees", "रुपये", "ਰੁਪਏ" };
        static private String[] _arrstrDolarOnly = { "Dollars", "Dollars", "डॉलर", "ਡਾਲਰ" };
        static private String[] _arrstrPaise = { "Paisa", "Paisa", "पैसे", "ਪੈਸੇ" };
        static private String[] _arrstrCent = { "Cent", "Cent", "सेंट", "ਸੇਂਟ" };
        static private String[] _arrstrPaiseOnly = { "Paisa", "Paisa", "पैसे", "ਪੈਸੇ" };
        static private String[] _arrstrCentOnly = { "Cent", "Cent", "सेंट", "ਸੇਂਟ" };
        static private String[] _arrstrDecimal = { "Point", "Point", "दशमलव", "ਦਸ਼ਮਲਵ" };
        static private String[] _arrstrPercent = { "Percent", "Percent", "प्रतिशत", "ਫ਼ੀਸਦੀ" };
        static private String[] _arrstrZero = { "Zero", "Zero", "शून्य", "ਸਿਫ਼ਰ" };
        #endregion
        #region Public Static Function
        public static String NumberToText(String pNumber, ConversionType pConversionType, Language pDisplayLanguage)
        {
            try
            {
                if (pConversionType == ConversionType.MonthName)
                {
                    Int16 _intMonthNumber = Convert.ToInt16(pNumber);
                    if (_intMonthNumber > 12 || _intMonthNumber < 1)
                        throw new Exception("Invalid month number");
                    else
                    {
                        _intMonthNumber -= 1;
                        return ConvertMonthIntoWords(_intMonthNumber, pDisplayLanguage);
                    }
                }
                else
                {
                    pNumber = pNumber.Replace('-', ' ').Trim();
                    if (pConversionType == ConversionType.Percentage && Convert.ToDouble(pNumber) > 100)
                        throw new Exception("Invalid number. Number should be less than or equal to 100");
                    else
                        return ConvertToWord(pNumber, pConversionType, pDisplayLanguage);
                }
            }
            catch (FormatException fx)
            {
                throw new Exception(fx.Message + " Input must be numeric value of upto 9 digits.");
            }
            catch (Exception)
            {
                throw;
            }


        }
        public static T ConvertFromDynamicObject<T>(dynamic data)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data, options));
        }

        public static IQueryable<T> OrderByNew<T>(this IQueryable<T> source, string columnName, string sortby)
        {
            bool isAscending = true;
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }

            if (sortby == "desc")
                isAscending = false;

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
        public static T ConvertFromDynamicStringToObject<T>(dynamic data)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Deserialize(data).ToString());
        }
        public static T ConvertFromDynamicObjectToString<T>(dynamic data)
        {
            return JsonSerializer.Serialize(data);
        }
        public static Dictionary<string, string> ConvertFromDynamicStringToDictionary(dynamic data)
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(Convert.ToString(data));
        }
        //public static T CloneNestedModel<T>(object data)
        //{
        //    return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data));
        //}
        #endregion
        #region Private Functions
        private static String ConvertMonthIntoWords(Int16 pMonthNumber, Language pDisplayLanguage)
        {
            try
            {
                return _arrstrMonthNames[Convert.ToInt16(pDisplayLanguage), pMonthNumber];
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static String ConvertToWord(String pAmount, ConversionType pConversionType, Language pDisplayLanguage)
        {
            try
            {
                StringBuilder _sbConv = new StringBuilder();
                if (Convert.ToDouble(pAmount) == 0)
                {
                    if (pConversionType == ConversionType.IndianRupees)
                        _sbConv.Append(_arrstrZero[Convert.ToUInt16(pDisplayLanguage)] + " " + _arrstrRupeeOnly[Convert.ToUInt16(pDisplayLanguage)]);
                    else if (pConversionType == ConversionType.Percentage)
                        _sbConv.Append(_arrstrZero[Convert.ToUInt16(pDisplayLanguage)] + " " + _arrstrPercent[Convert.ToUInt16(pDisplayLanguage)]);
                    return _sbConv.ToString();
                }

                String _strRupies = pAmount;
                String _strPaise = "";
                if (_strRupies.Contains("."))
                {
                    String[] _arrRupee = _strRupies.Split('.');
                    _strRupies = _arrRupee[0];
                    _strPaise = _arrRupee[1];
                }
                Int32 _intLen = _strRupies.Length;
                String _strTempNumber = "";
                if (_strRupies.Length > 9)
                {
                    _strTempNumber = _strRupies.Substring(0, _intLen - 7);
                    _strTempNumber = ConvertToWord(_strTempNumber, ConversionType.None, pDisplayLanguage);
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber + " Crore ");
                    _intLen = 7;
                    _strRupies = _strRupies.Substring(_strRupies.Length - 7, _intLen);
                    //throw new Exception("This function can convert up to 9 digit numbers only");
                }
                if (_intLen == 8 || _intLen == 9)
                {
                    if (_intLen == 8)
                    {
                        _strRupies = "0" + _strRupies;
                        _intLen = _strRupies.Length;
                    }
                    _strTempNumber = _strRupies.Substring(0, _intLen - 7);
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strTempNumber)];
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber + " " + _arrstrCrore[Convert.ToUInt16(pDisplayLanguage)].ToString() + " ");
                    _intLen = _intLen - 2;
                    _strRupies = _strRupies.Substring(2, _intLen);
                }

                if (_intLen == 6 || _intLen == 7)
                {
                    if (_intLen == 6)
                    {
                        _strRupies = "0" + _strRupies;
                        _intLen = _strRupies.Length;
                    }
                    _strTempNumber = _strRupies.Substring(0, _intLen - 5);
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strTempNumber)];
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber.ToString() + " " + _arrstrLac[Convert.ToUInt16(pDisplayLanguage)].ToString() + " ");
                    _intLen = _intLen - 2;
                    _strRupies = _strRupies.Substring(2, _intLen);
                }

                if (_intLen == 4 || _intLen == 5)
                {
                    if (_intLen == 4)
                    {
                        _strRupies = "0" + _strRupies;
                        _intLen = _strRupies.Length;
                    }
                    _strTempNumber = _strRupies.Substring(0, _intLen - 3);
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strTempNumber)];
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber.ToString() + " " + _arrstrThousand[Convert.ToUInt16(pDisplayLanguage)].ToString() + " ");
                    _intLen = _intLen - 2;
                    _strRupies = _strRupies.Substring(2, _intLen);
                }

                if (_intLen == 3)
                {
                    _strTempNumber = _strRupies.Substring(0, _intLen - 2);
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strTempNumber)];
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber.ToString() + " " + _arrstrHundred[Convert.ToUInt16(pDisplayLanguage)].ToString() + " ");
                    _intLen = _intLen - 1;
                    _strRupies = _strRupies.Substring(1, _intLen);
                }

                if (_intLen == 2 || _intLen == 1)
                {
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strRupies)];
                    if (_strTempNumber != "")
                        _sbConv.Append(_strTempNumber.ToString());
                }

                if (_strPaise != "")
                {
                    if (_strPaise.Length == 1)
                        _strPaise = _strPaise + "0";
                    else if (_strPaise.Length > 2)
                        _strPaise = _strPaise.Substring(0, 2);
                    _strTempNumber = _arrstrNumbers[Convert.ToUInt16(pDisplayLanguage), Convert.ToUInt16(_strPaise)];
                    if (_strTempNumber != "")
                    {
                        if (pConversionType == ConversionType.IndianRupees)
                            _sbConv.Append(" " + _arrstrRupee[Convert.ToUInt16(pDisplayLanguage)] + " " + _strTempNumber.ToString() + " " + _arrstrPaiseOnly[Convert.ToUInt16(pDisplayLanguage)]);
                        else if (pConversionType == ConversionType.Percentage)
                            _sbConv.Append(" " + _arrstrDecimal[Convert.ToUInt16(pDisplayLanguage)] + " " + _strTempNumber.ToString() + " " + _arrstrPercent[Convert.ToUInt16(pDisplayLanguage)]);
                        else if (pConversionType == ConversionType.None)
                            _sbConv.Append(" " + _arrstrDecimal[Convert.ToUInt16(pDisplayLanguage)] + " " + _strTempNumber.ToString());
                    }
                    else
                    {
                        if (pConversionType == ConversionType.IndianRupees)
                            _sbConv.Append(" " + _arrstrRupeeOnly[Convert.ToUInt16(pDisplayLanguage)]);
                        else if (pConversionType == ConversionType.Percentage)
                            _sbConv.Append(" " + _arrstrPercent[Convert.ToUInt16(pDisplayLanguage)]);
                    }
                }
                else
                {
                    if (pConversionType == ConversionType.IndianRupees)
                        _sbConv.Append(" " + _arrstrRupeeOnly[Convert.ToUInt16(pDisplayLanguage)]);
                    else if (pConversionType == ConversionType.Percentage)
                        _sbConv.Append(" " + _arrstrPercent[Convert.ToUInt16(pDisplayLanguage)]);
                }
                return _sbConv.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string MaskMobileNumber(string mobileNumber = "-")
        {
            if (string.IsNullOrEmpty(mobileNumber) == false)
            {
                if (mobileNumber.Length == 10)
                    return mobileNumber.Replace(mobileNumber.Substring(0, 3) + mobileNumber.Substring(3, 4), (mobileNumber.Substring(0, 3) + "XXXX"));
                else
                    return mobileNumber;
            }
            else
                return "-";
        }
        public static string MaskEmailAddress(string emailAddress = "-")
        {
            if (string.IsNullOrEmpty(emailAddress) == false)
            {
                string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
                return Regex.Replace(emailAddress, pattern, m => new string('X', m.Length));
            }
            else
                return "-";
        }
        #endregion
    }
}
