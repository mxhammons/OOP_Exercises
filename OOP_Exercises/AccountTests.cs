using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exersizes
{
    [TestFixture]
    public class AccountTests
    {
        Type AccountType;
        dynamic GetAccount()
        {
            return Activator.CreateInstance(AccountType);
        }
        [OneTimeSetUp]
        public void FindAccount()
        {
            AccountType = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == "Account");
        }
        [Test]
        public void AccountClassExists()
        {
            Assert.IsNotNull(AccountType);
        }
        [Test]
        public void NamePropertyExistsWithADefaultOfBlankString()
        {
            Assert.AreEqual(String.Empty, GetAccount().Name, "Name should default to String.Empty");
        }
        [Test]
        public void BalancePropertyExistsWithDefaultOfZero()
        {
            Assert.AreEqual(0m, GetAccount().Balance, "Balance property should be a decimal that defaults to 0");
        }
        [Test]
        public void BalancePropertyIsReadOnly()
        {
            Assert.IsFalse(AccountType.GetProperty("Balance").CanWrite);
        }
        [Test]
        public void CreditFunctionExists()
        {
            Assert.NotNull(AccountType.GetMethod("Credit"));
        }
        [Test]
        public void CreditFunctionIncrementsBalance()
        {
            var acct = GetAccount();
            acct.Credit(10.5m);
            Assert.AreEqual(10.5, acct.Balance);
        }
        [Test]
        public void CreditFunctionReturnsNewBalance()
        {
            var acct = GetAccount();
            decimal bal = acct.Credit(10.5m);
            Assert.AreEqual(10.5, bal);
        }
        [Test]
        public void DebitFunctionExists()
        {
            Assert.NotNull(AccountType.GetMethod("Debit"));
        }
        [Test]
        public void DebitFunctionIncrementsBalance()
        {
            var acct = GetAccount();
            acct.Credit(10.5m);
            acct.Debit(10.5m);
            Assert.AreEqual(0, acct.Balance);
        }
        [Test]
        public void DebitFunctionReturnsNewBalance()
        {
            var acct = GetAccount();
            acct.Credit(10.5m);
            decimal bal = acct.Debit(10.5m);
            Assert.AreEqual(0, bal);
        }
        [Test]
        public void DebitFunctionThrowsExceptionWhenGivenNegative()
        {
            Assert.Throws<Exception>(() => GetAccount().Debit(-1m));
        }
        [Test]
        public void DebitFunctionThrowsExceptionWhenGivenNumberGreaterThanBalance()
        {
            Assert.Throws<Exception>(() => GetAccount().Debit(1m));
        }
        [Test]
        public void TransferToFunctionExists()
        {
            Assert.NotNull(AccountType.GetMethod("TransferTo"));
        }
        [Test]
        public void TransferToFunctionDecrementsAccount()
        {
            var acct = GetAccount();
            acct.Credit(10m);
            acct.TransferTo(GetAccount(), 5m);
            Assert.AreEqual(5, acct.Balance);
        }
        [Test]
        public void TransferToFunctionReturnsNewBalance()
        {
            var acct = GetAccount();
            acct.Credit(10m);
            decimal newBal = acct.TransferTo(GetAccount(), 5m);
            Assert.AreEqual(5, newBal);
        }
        [Test]
        public void TransferToFunctionIncrementsBalanceOfTransferee()
        {
            var acct = GetAccount();
            acct.Credit(10m);
            var acct2 = GetAccount();
            acct.TransferTo(acct2, 5m);
            Assert.AreEqual(5, acct2.Balance);
        }
        [Test]
        public void TransferToThrowsExceptionWhenGivenNegativeNumber()
        {
            var acct = GetAccount();
            acct.Credit(10m);
            Assert.Throws<Exception>(() => acct.TransferTo(GetAccount(), -5m));
        }
        [Test]
        public void TransferToThrowsExceptionWhenGivenAmountGreaterThanBalance()
        {
            Assert.Throws<Exception>(() => GetAccount().TransferTo(GetAccount(), 10m));
        }
        public void ToStringReturnsFormattedValue()
        {
            var acct = GetAccount();
            acct.Credit(100m);
            acct.Name = "Ringo";
            Assert.AreEqual($"Account[name=Ringo, balance=100]", acct.ToString(), "Account[name={this.Name}, balance={this.Balance}]");
        }
        [Test]
        public void HasConstructorWithNameParameter()
        {
            dynamic acct = Activator.CreateInstance(AccountType, "George");
            Assert.AreEqual("George", acct.Name);
            Assert.AreEqual(0, acct.Balance, "Balance should default to 0 when using the name only constructor");
        }
        [Test]
        public void HasConstructorWithNameAndBalanceParameters()
        {
            dynamic acct = Activator.CreateInstance(AccountType, "John", 9m);
            Assert.AreEqual("John", acct.Name);
            Assert.AreEqual(9, acct.Balance);
        }
        [Test]
        public void ConstructorThrowsExceptionForNegativeBalance()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(() => Activator.CreateInstance(AccountType, string.Empty, -1m));
        }
    }
}
