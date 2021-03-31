using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;
using WpfSMSApp.View.User;

namespace WpfSMSApp.View.Account
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeactiveUser : Page
    {
        public DeactiveUser()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // error 메세지 hidden
                //콤보박스 초기화
                // 그리드바인딩
                List<Model.User> users = Logic.DataAccess.GetUsers();
                this.DataContext = users;
                
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 EditAccount Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // 이전화면으로 돌아감
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판결하는 플래그

            if (GrdData.SelectedItem == null)
            {
                await Commons.ShowMessageAsync("오류", "비활성화할 사용자를 선택하세요");
                //MessageBox.Show("비활성화할 사용자를 선택하세요.");
                return;
            }

            if (isValid)
            {
                try
                {
                    var user = GrdData.SelectedItem as Model.User;
                    user.UserActivated = false; // 사용자 비활성화

                    var result = Logic.DataAccess.SetUser(user);
                    if (result == 0)
                    {
                        await Commons.ShowMessageAsync("오류", "사용자 수정에 실패했습니다.");
                        return;
                    }
                    else
                    {
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                // 선택된 값이 입력창에 나오도록
                var user = GrdData.SelectedItem as Model.User;

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 GrdData_SelectedCellsChanged = {ex}");
            }
        }
    }
}
