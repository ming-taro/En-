package ui.panel;

import java.awt.Dimension;
import java.awt.Graphics;
import java.sql.SQLException;

import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JPasswordField;
import javax.swing.JTextField;

import controller.ProfileDAO;
import model.Profile;
import ui.UICreator;

public class EditionPanel extends SignUpPanel {
	private ProfileDAO profileDAO;
	
	public EditionPanel() {
		profileDAO = ProfileDAO.GetInstance();
	}
	
	private String getFirstValue(String profile, String delimiter) { 
		int endIndex = profile.indexOf(delimiter);//ex: birth -> 2000.1.2 -> 출생 연도: 첫번째 '.' 이전까지의 값 = 2000
											      //ex: phoneNumber -> 010-1234-5678 -> 첫 번재 번호 = 010
		return profile.substring(0, endIndex);
	}
	
	private String getSecondValue(String profile, String delimiter) {
		int beginIndex = profile.indexOf(delimiter) + 1;  //ex: birth -> 2000.1.2 -> 출생 월: 첫번째 '.' ~ 마지막 '.' 이전까지의 값 = 1
		int endIndex = profile.lastIndexOf(delimiter);    //ex: phoneNumber -> 010-1234-5678 -> 두 번째 번호 = 1234
		
		return profile.substring(beginIndex, endIndex);
	}
	
	private String getThirdValue(String profile, String delimiter) {  
		int beginIndex = profile.lastIndexOf(delimiter) + 1;//ex: birth -> 2000.1.2 -> 출생 일: 마지막 '.'+ 1 ~ = 2
	      										    //ex: phoneNumber -> 010-1234-5678 -> 세 번째 번호 = 5678		
		return profile.substring(beginIndex);
	}
	
	public void setMemberInformation(String memberId) {    //DB에 저장되어 있는 사용자의 정보를 불러옴
		Profile profile = null;
		
		try {
			profile = profileDAO.getMemberProfile(memberId);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		String birth = profile.getBirth();
		String phoneNumber = profile.getPhoneNumber();
		
		idTextField.setText(profile.getId());                                  //아이디
		
		nameTextField.setText(profile.getName());                              //이름
		
		yearTextField.setText(getFirstValue(birth, "."));                      //출생일
		monthcomboBox.setSelectedItem(getSecondValue(birth, "."));
		dayTextField.setText(getThirdValue(birth, "."));

		sexComboBox.setSelectedItem(profile.getSex());                         //성별
		
		zipCodeTextField.setText(profile.getZipCode());                        //주소
		roadNameAddressTextField.setText(profile.getRoadNameAddress());
		detailAddressTextField.setText(profile.getDetailAddress());
		
		phoneNumberComboBox.setSelectedItem(getFirstValue(phoneNumber, "-"));  //휴대폰 번호
		middleNumberTextField.setText(getSecondValue(phoneNumber, "-"));;
		lastFourDigitsTextField.setText(getThirdValue(phoneNumber, "-"));
		
		emailTextField.setText(profile.getEmail());
		
	}
	
}
