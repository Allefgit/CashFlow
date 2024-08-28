using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact] //Informa que a seguinte fun��o � um teste
    public void Success()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBiulder.Build();

        //Act
        var result = validator.Validate(request);

        //Asser
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBiulder.Build();
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Asser
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBiulder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        //Act
        var result = validator.Validate(request);

        //Asser
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }

    [Theory] // Usado para quando temos que testar com mais de um valor 
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Greater_Than_Zero(decimal amount)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBiulder.Build();
        request.Amount = amount;

        //Act
        var result = validator.Validate(request);

        //Asser
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBiulder.Build();
        request.PaymentType = (PaymentsTypes)8;

        //Act
        var result = validator.Validate(request);

        //Asser
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.PAYMENT_TYPE_INVALID));
    }
}
