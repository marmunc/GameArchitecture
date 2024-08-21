using System;

namespace Architecture
{
    public static class Bank
    {

        private static BankInteractor _bankInteractor;

        public static event Action OnBankInitializedEvent;

        public static int Coins
        {
            get
            {
                CheckClass();
                return _bankInteractor.Coins;
            }
        }
        public static bool IsInitialized {  get; private set; }

        public static void Initialize(BankInteractor bankInteractor)
        {
            _bankInteractor = bankInteractor;
            IsInitialized = true;
            OnBankInitializedEvent?.Invoke();
        }

        public static bool IsEnoughtCoins(int value)
        {
            CheckClass();
            return _bankInteractor.IsEnoughtCoins(value);
        }

        public static void AddCoins(object sender, int value)
        {
            CheckClass();
            _bankInteractor.AddCoins(sender, value);
        }

        public static void Spend(object sender, int value)
        {
            CheckClass();
            _bankInteractor.Spend(sender, value);
        }

        private static void CheckClass()
        {
            if (!IsInitialized)
            {
                throw new System.Exception("Bank is not initilize yet");
            }
        }

    }
}