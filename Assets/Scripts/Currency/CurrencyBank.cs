using System;
using System.Collections.Generic;

namespace SunnyDoggyClicker.Currencies {
    public sealed class CurrencyBank {
        private readonly Dictionary<string, float> _balances = new Dictionary<string, float>();
        private readonly Dictionary<string, float> _balanceLimits = new Dictionary<string, float>();

        public Action<string, float> BalanceChanged;

        public bool ExecuteTransaction(CurrencyTransaction transaction) {
            if (!ValidateTransaction(transaction)) {
                return false;
            }
            if (transaction.Type == TransactionType.Exchange) {
                return ProcessExchange(transaction.SourceCurrency, transaction.TargetCurrency, transaction.Amount, transaction.ExchangeRate);
            }
            return ProcessSingle(transaction.Currency, transaction.Amount * (transaction.Type == TransactionType.Add ? 1 : -1));
        }

        private bool ValidateTransaction(CurrencyTransaction transaction) {
            if (transaction.Amount <= 0) {
                return false;
            }
            return transaction.Type switch {
                TransactionType.Add => !string.IsNullOrEmpty(transaction.Currency) && CheckAddLimit(transaction.Currency, transaction.Amount),
                TransactionType.Subtract => HasCurrency(transaction.Currency) && GetBalance(transaction.Currency) >= transaction.Amount,
                TransactionType.Exchange => HasCurrency(transaction.SourceCurrency)
                    && HasCurrency(transaction.TargetCurrency)
                    && GetBalance(transaction.SourceCurrency) >= transaction.Amount
                    && CheckAddLimit(transaction.TargetCurrency, transaction.Amount * transaction.ExchangeRate),
                _ => false
            };
        }

        private bool CheckAddLimit(string currency, float amountToAdd) {
            if (!_balanceLimits.TryGetValue(currency, out var limit)) {
                return true;
            }

            return GetBalance(currency) + amountToAdd <= limit;
        }

        private bool ProcessSingle(string currency, float amount) {
            _balances[currency] += amount;
            BalanceChanged?.Invoke(currency, _balances[currency]);
            return true;
        }

        private bool ProcessExchange(string source, string target, float amount, float rate) {
            return ProcessSingle(source, -amount) && ProcessSingle(target, amount * rate);
        }

        public float GetBalance(string currency) => _balances.TryGetValue(currency, out var balance) ? balance : 0;
        public bool HasCurrency(string currency) => _balances.ContainsKey(currency);
        public bool RegisterCurrency(string currency) => _balances.TryAdd(currency, 0);
        public bool RegisterCurrency(string currency, float balanceLimit) => _balances.TryAdd(currency, 0) && _balanceLimits.TryAdd(currency, balanceLimit);
    }

    public enum TransactionType { Add, Subtract, Exchange }

    public sealed class CurrencyTransaction {
        public TransactionType Type { get; }
        public string Currency { get; }
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public float Amount { get; }
        public float ExchangeRate { get; }

        private CurrencyTransaction(TransactionType type, string currency, float amount) {
            Type = type;
            Currency = currency;
            Amount = amount;
        }

        private CurrencyTransaction(string source, string target, float amount, float rate) {
            Type = TransactionType.Exchange;
            SourceCurrency = source;
            TargetCurrency = target;
            Amount = amount;
            ExchangeRate = rate;
        }

        public static CurrencyTransaction CreateAdd(string currency, float amount) => new CurrencyTransaction(TransactionType.Add, currency, amount);
        public static CurrencyTransaction CreateSubtract(string currency, float amount) => new CurrencyTransaction(TransactionType.Subtract, currency, amount);
        public static CurrencyTransaction CreateExchange(string source, string target, float amount, float rate) => new CurrencyTransaction(source, target, amount, rate);
    }
}