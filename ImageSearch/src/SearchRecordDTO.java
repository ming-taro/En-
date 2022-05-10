
public class SearchRecordDTO {
	private String searchWord;
	private String date;
	public SearchRecordDTO(String searchWord, String date) {
		this.searchWord = searchWord;
		this.date = date;
	}
	public String getSearchWord() {
		return searchWord; 
	}
	public String getDate() {
		return date; 
	}
}
