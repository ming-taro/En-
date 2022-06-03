package utility;

public class Constants {
	public static final int WIDTH = 1100;
	public static final int HEIGHT = 700;

	public static final int SEARCH_FRAME_WIDTH = 500;
	public static final int SEARCH_FRAME_HEIGHT = 600;
	public static final int SEARCH_PANEL_WIDTH = 400;
	
	public static final boolean IS_MATCH = true;
	
	public static final String ID_REGEX = "^[a-z0-9]{5,20}$";
	public static final String PASSWORD_REGEX = "^[a-zA-Z0-9]{8,16}$";
	public static final String NAME_REGEX = "^[a-zA-Z가-힣]{1,40}$";
	public static final String BIRTH_REGEX = "^(1|2)[0-9]{3}\\.(0[1-9]|1[0-2])\\.(0[1-9]|(1|2)[0-9]|3[0-1])$";
	public static final String SEX_REGEX = "(여|남)"; 
	public static final String PHONE_NUMBER_REGEX = "^(01)[0|1|6|9]{1}-[0-9]{4}-[0-9]{4} || (01)[1|6|7|8|9|]{1}-[0-9]{3}-[0-9]{4})";
	public static final String EMAIL_REGEX = "\\w+@\\w.\\w+(\\w.\\w+)?$";
	
	public static final String QUERY_FOR_MEMBER_PROFILE = "select * from memberList where id = %s";
	public static final String QUERY_TO_ADD_MEMBER =
			"insert into memberlist(id, password, name, birth, sex, "
		  + "zipCode, roadNameAddress, detailAddress, phoneNumber, email) "
		  + "values('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s');";
}
