using System;
namespace Business.Constants
{
	public class Messages
	{
		public static string Added = "Eklendi.";

		public static string Updated = "Güncellendi.";

        public static string Deleted = "Silindi.";

        public static string Listed = "Listelendi.";

        public static string WrongCurrentPassword = "Eski şifre yanlış";

		public static string PasswordChanged = "Şifreniz Başarıyla Değiştirildi";

		public static string NameIsNotAvailable = "Bu İsim Zaten Mevcut.";
        
        public static string NameExist = "Geçerli İsim";

        public static string UserAndClaimAvailable = "Bu Kullanıcıya Bu Yetki Daha Önce Atanmış.";
        
        public static string OperationClaimNotExist = "Seçtiğiniz Yetki Bilgisi Bulunamadı.";

        public static string UserNotExist = "Seçtiğiniz Yetki Bilgisi Bulunamadı.";

        public static string AuthorizationDenied = "Yetkiniz Yok.";

        public static string UpdatedEmailParameter = "Mail Güncellendi.";

        public static string EmailSendSuccesiful = "Mail Gönderildi.";

        public static string UpdatedUser = "Kullanıcı kaydı başarıyla güncellendi";
        public static string DeletedUser = "Kullanıcı kaydı başarıyla silindi";
        public static string UserAlreadyConfirm = "Kullanıcı zaten onaylanmış";
        public static string UserConfirmIsSuccesiful = "Kullanıcı maili başarıyla onaylandı";
        public static string AlreadySendForgotPasswordMail = "Şifre unuttum maili 5 dakikada bir gönderilebilir ve süresi geçmemiş bir mail isteği mevcut";
        public static string ForgotPasswordMailSendSuccessiful = "Şifremi unuttum maili başarıyla gönderildi";
        public static string ForgotPasswordValueIsUsed = "Şifre yenileme linki daha önce kullanılmış";
        public static string ForgotPasswordValueTimeIsEnded = "Şifre yenileme linkini süresi dolmuş";
        public static string ConfirmUserMailSendSuccessiful = "Kullanıcı onaylama maili başarıyla gönderildi";
        public static string UserNotFound = "Kullanıcı maili bulunamadı";
        public static string ForgotPasswordValueIsNotValid = "Şifre yenileme linki geçerli değil";

        public static string OperationClaimSetExist = "Bu kullanıcıya bu yetki daha önce atanmış!";
    }
}

