package Model;

public class EquationDTO {
	private String firstValue;
	private String secondValue;
	private String operator;
	private String expression;
	private String result;
	
	public EquationDTO() {
		InitValue();
	}
	public void InitValue() {
		firstValue = "";
		secondValue = "";
		operator = "";
		result = "";
	}
	public void setFirstValue(String number) {
		firstValue = number;
	}
	public void setSecondValue(String number) {
		secondValue = number;
	}
	public void setOperator(String operator) {
		this.operator = operator;
	}
	public String getFirstValue() {
		return firstValue;
	}
	public String getSecondValue() {
		return secondValue;
	}
	public String getOperator() {
		return operator;
	}
	public void setResult(String result) {
		this.result = result;
	}
	public String getResult() {
		return result;
	}
	@Override
    public String toString() {
        return firstValue + " " + operator + " " + secondValue + " = ";
    }
}
