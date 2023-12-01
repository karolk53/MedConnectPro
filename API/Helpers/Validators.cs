namespace API.Helpers
{
    public class Validators
    {

        public static bool ValidatePWZ(string pwz)
        {
            if (pwz.Length != 7)
            {
                return false;
            }

            for (int i = 0; i < 7; i++)
            {
                if (pwz[i] < '0' || pwz[i] > '9')
                {
                    return false;
                }
            }

            int sum = 0;
            for (int i = 1; i <= 6; i++)
            {
                int digit = pwz[i] - '0';
                sum += digit * i;
            }

            int controlDigit = sum % 11;
            int expectedControlDigit = pwz[0] - '0';

            if (controlDigit == 10)
            {
                controlDigit = 0;
            }

            return controlDigit == expectedControlDigit;
        }


        public static bool ValidatePesel(string pesel)
        {
            if (pesel.Length != 11)
            {
                return false;
            }

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                if (char.IsDigit(pesel[i]))
                {
                    int digit = int.Parse(pesel[i].ToString());
                    sum += digit * weights[i];
                }
                else
                {
                    return false; // Nieprawidłowy znak
                }
            }

            int controlDigit = 10 - (sum % 10);
            if (controlDigit == 10)
            {
                controlDigit = 0;
            }

            int peselControlDigit;
            if (int.TryParse(pesel[10].ToString(), out peselControlDigit))
            {
                if (controlDigit == peselControlDigit)
                {
                    return true;
                }
            }

            return false;
        }
    }

}