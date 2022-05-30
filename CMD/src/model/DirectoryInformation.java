package model;

public class DirectoryInformation {
	private String lastModifiedDate;
	private String category;
	private String fileSize;
	private String fileName;
	
	public void setLastModifiedDate(String lastModifiedDate) {
		this.lastModifiedDate = lastModifiedDate;
	}
	public void setCategory(String category) {
		this.category = category;
	}
	public void setFileSize(String fileSize) {
		this.fileSize = fileSize;
	}
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}
	@Override
	public String toString() {
		return lastModifiedDate + "     " + category + "     "
				+ fileSize + " " + fileName + "\n";
	}
}
