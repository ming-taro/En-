package view;

public class ClearScreen {
	public void execut() {
		StringBuilder stringBuilder = new StringBuilder();
		
		for(int row = 0;  row < 40; row++) {
			stringBuilder.append("\n");
		}
		System.out.print(stringBuilder);
	}
}
