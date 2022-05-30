package model;

public class DirectoryInformation {
	private String lastModifiedDate;
	private String category;
	private String fileSize;
	private String fileName;
	
	public void setDirectoryInformation(
		String lastModifiedDate, String category, String fileSize, String fileName) {
		this.lastModifiedDate = lastModifiedDate;
		this.category = category;
		this.fileSize = fileSize;
		this.fileName = fileName;
	}
	public String getFileSize() {
		return fileSize;
	}
	public String getCategory() {
		return category;
	}
	@Override
	public String toString() {
		return lastModifiedDate + category
				+ fileSize + " " + fileName + "\n";
	}
}
