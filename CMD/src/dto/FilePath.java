package dto;

public class FilePath {
	private String currentPath;
	private String firstFile;
	private String secondFile;
	
	public FilePath() {
		currentPath = "";
		firstFile = "";
		secondFile = "";
	}
	public void setCurrentPath(String currentPath) {
		this.currentPath = currentPath;
	}
	public String getCurrentPath() {
		return currentPath;
	}
	public void setFirstFile(String firstFile) {
		this.firstFile = firstFile;
	}
	public String getFirstFile() {
		return firstFile;
	}
	public void setSecondFile(String secondFile) {
		this.secondFile = secondFile;
	}
	public String getSecondFile() {
		return secondFile;
	}
	public void init() {
		currentPath = "";
		firstFile = "";
		secondFile = "";
	}
}
