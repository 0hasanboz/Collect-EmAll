using System;
using Base;
using Core;
using Statics;

namespace Controllers
{
    public class AccountController : IInitializable
    {
        public Action<int> OnCoinsChanged;
        public Action<int> OnLevelChanged;

        private AccountData _accountData;
        private IDatabase _database;

        public void Initialize(IContainer container)
        {
            _database = container.GetComponent<IDatabase>();
            _accountData = _database.GetData<AccountData>(DatabaseKeys.AccountDataKey);
        }

        public void LevelUp()
        {
            _accountData.level++;
            _database.Save();
            OnLevelChanged?.Invoke(_accountData.level);
        }

        public void AddCoins(int amount)
        {
            _accountData.coinAmount += amount;
            _database.Save();
            OnCoinsChanged?.Invoke(_accountData.coinAmount);
        }

        public bool SpendCoins(int amount)
        {
            if (_accountData.coinAmount <= 0 || _accountData.coinAmount < amount)
            {
                return false;
            }

            _accountData.coinAmount -= amount;
            _database.Save();
            OnCoinsChanged?.Invoke(_accountData.coinAmount);
            return true;
        }

        public int GetCoinAmount()
        {
            return _accountData.coinAmount;
        }

        public int GetLevel()
        {
            return _accountData.level;
        }
    }
}