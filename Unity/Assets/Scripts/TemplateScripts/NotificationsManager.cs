using UnityEngine;
using System.Collections;
using System;
using LocalNotification = UnityEngine.iOS.LocalNotification;
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;

/*Scene:N/A
 *Object:NottificationsManager
 *Opis:Klasa koja sadrzi funkcije potrebne za setovanje i uklanjanje lokalnih notifikacija.
 *Napomena: na IOS-u je potrebno pre setovanja lokalne notifikacije( prvi put ) OBAVEZNO registorvati aplikaciju za lokalne notifikacije.
 *
 *
 */
public class NotificationsManager : MonoBehaviour {
	
	/// <summary>
	/// Setuje lokalnu notifikaciju
	/// </summary>
	/// <param name="timeOffset">Vreme u sekundama od tekuceg vremena kada treba prikazati notifikaciju</param>
	/// <param name="title">Naslov notifikacije</param>
	/// <param name="message">Telo (poruka) notifkacije</param>
	/// <param name="id">ID notifikacije.Za IOS ovo predstavlja redni broj na badge-u.</param>
	
	void Start()
	{
		//TODO: NOTIFIKACIJE
		//int sec= (Mathf.FloorToInt(  (float)  new System.TimeSpan(48,0,0).TotalSeconds));
		if (!PlayerPrefs.HasKey ("LastTime")) {
			// App started for first time
			PlayerPrefs.SetString("LastTime",DateTime.Now.ToString());

			SetNottification (86400, "Car Wash Salon Kids Game", "Please, help me clean my car", 11223380);
		}
		else
		{

			string[] sNotifications = new string[] {
					"Please, help me clean my car",
					"Help policeman and wash his car",
					"Car Wash today?",
					"Good day for Car Wash!"
			};
			int i = Mathf.FloorToInt( UnityEngine.Random.Range(0,4));
			CancelNottificationWithID(11223380);
			if(Rate.appStartedNumber == 1) SetNottification (86400, "Car Wash Salon Kids Game", sNotifications[i], 11223380);
			else 	SetNottification (172800, "Car Wash Salon Kids Game", sNotifications[i], 11223380);

			//Debug.Log(Rate.appStartedNumber+ ":"+ sNotifications[i]);
		}
	}
	
	public void SetNottification(int timeOffset,string title,string message,int id)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{	
				obj_Activity.Call("SendNotification",timeOffset.ToString(),message,id);
			}
		}
		#endif
		
		#if UNITY_IOS && !UNITY_EDITOR
		LocalNotification notification = new LocalNotification ();
		notification.fireDate = System.DateTime.Now.AddSeconds(timeOffset);
		notification.alertAction = title;
		notification.alertBody = message;
		notification.hasAction = false;
		notification.applicationIconBadgeNumber=id;
		NotificationServices.ScheduleLocalNotification (notification);
		#endif
	}
	/// <summary>
	/// Uklanja sve setovane  lokalne notifikacije (Samo IOS)
	/// </summary>
	public void CancelAllNotifications()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		
		//Empty notification to clear badge number
		LocalNotification l = new LocalNotification ();
		l.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (l);
		NotificationServices.CancelAllLocalNotifications();
		NotificationServices.ClearLocalNotifications();
		NotificationServices.ClearLocalNotifications ();
		#endif
		
	}
	/// <summary>
	/// Uklanja setovanu lokalnu notifikaciju sa odgovarajucim ID-jem. (samo Android)
	/// </summary>
	/// <returns><c>true</c> if this instance cancel nottification with I the specified id; otherwise, <c>false</c>.</returns>
	/// <param name="id">Identifier.</param>
	public void CancelNottificationWithID(int id)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{
				obj_Activity.Call("CancelNotification",id);
				
			}
		}
		#endif
		
	}
	/// <summary>
	/// Registruje aplikaciju za setovanje i primanje lokalnih notifikacija na IOS-u.
	/// </summary>
	public void RegisterForLocalNottifications()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		//za tip notifikacije promeniti argument funkcije
		//ovo je default poziv
		NotificationServices.RegisterForNotifications(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
		
		#endif
	}
}
