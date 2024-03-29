package utility;

public class Constants {
	public static final int WIDTH = 1100;
	public static final int HEIGHT = 700;

	public static final int SEARCH_FRAME_WIDTH = 500;
	public static final int SEARCH_FRAME_HEIGHT = 600;
	public static final int SEARCH_PANEL_WIDTH = 400;
	
	public static final int YES_OPTION = 0;
	
	public static final String SIGN_UP_PANEL = "SignUpPanel";
	public static final String MAIN_PANEL = "MainPanel";
	public static final String SIGN_UP_COMPLETION_PANEL = "SignUpCompletionPanel";
	public static final String USER_MODE_PANEL = "UserModePanel";
	public static final String EDITION_PANEL = "EditionPanel";
	public static final String EDITION_MODE_PANEL = "EditionModePanel";
	
	public static final boolean IS_MATCH = true;
	public static final boolean IS_MEMBER_IN_LIST = true;
	public static final boolean IS_DUPLICATE_ID = true;
	public static final boolean IS_DUPLICATE_PHONE_NUMBER = true;
	
	public static final String ID_REGEX = "^[a-z0-9]{5,20}$";
	public static final String PASSWORD_REGEX = "^[a-zA-Z0-9]{8,16}$";
	public static final String NAME_REGEX = "^[a-zA-Z가-힣]{1,40}$";
	public static final String BIRTH_REGEX = 
			"^(1|2)[0-9]{3}\\.([1-9]|1[0-2])\\.([1-9]|(1|2)[0-9]|3[0-1])$";
	public static final String SEX_REGEX = "(여|남)"; 
	public static final String PHONE_NUMBER_REGEX =
			"^((01)(0|1|6|9){1}-[0-9]{4}-[0-9]{4})||^((01)(1|6|7|8|9|){1}-[0-9]{3}-[0-9]{4})";
	public static final String EMAIL_REGEX = "\\w+@\\w+.\\w+$";
	
	public static final String QUERY_FOR_MEMBER_PROFILE = 
			"select * from memberList where id = '%s';";
	public static final String QUERY_TO_ADD_MEMBER =
			"insert into memberlist(id, password, name, birth, sex, "
		  + "zipCode, roadNameAddress, detailAddress, phoneNumber, email) "
		  + "values('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s');";
	public static final String QUERY_TO_UPDATE_MEMBER_INFORMATION =
			"update memberlist set password='%s', zipCode='%s', roadNameAddress='%s',"
			+ " detailAddress='%s', phoneNumber='%s', email='%s' where id='%s'";
	public static final String QUERY_TO_CHECK_IF_MEMBER_IS_IN_LIST =
			"select id from memberList where id = '%s' and password = '%s';";
	public static final String QUERY_TO_DELETE_MEMBER =
			"delete from memberList where id = '%s';";
	public static final String QUERY_TO_CHECK_IF_DUPLICATE_ID = 
			"select id from memberList where id = '%s';";
	public static final String QUERY_TO_CHECK_IF_DUPLICATE_PHONE_NUMBER = 
			"select phoneNumber from memberList where phoneNumber = '%s';";
}
