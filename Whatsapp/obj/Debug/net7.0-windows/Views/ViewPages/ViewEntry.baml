   M S B A M L   `   `   `  ����C  ?Whatsapp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Whatsapp.Views.ViewPagesWhatsapp.Views.ViewPages  5      (    "Whatsapp.Views.ViewPages.ViewEntry   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Whatsapp.Views.ViewPages     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
      -   5        $
��page��5	        N� 
Background
  ����  N� Title$ 	ViewEntry��5      .��5      ��  �� CornerRadius$	 100��6   ��5   
   �� 5       �� ImageSource$ \Images\background.jpg��6   ._�5   
   � .+�5      �� $��40,40q�6d   $��Right=�6t   $	��Top=�6�      MaterialDesignIconButton#��[  5       � ToolTip$ MaterialDesignIconButton��5      ��5      �� *CloseCommand+��66   ��  �� ElementName$
 page��{�5      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window.��5      '  !MaterialDesignThemes.Wpf.PackIcon  $��50��6*   $��40��66   
��� � �   Kind$ Close=�5      5      �� 5      	 J� Opacity$	 50Q�6   $��Center=�6'   $	��500��6B   $	��400��6O   
������ $��350,104,350,0q�6u   .+�5      G� -   5        $��GmailTextBox��5       # MaterialDesignFilledPasswordBox#��[ 6/   $��20(�5   %   $��Center=�63   
�������
��� � �$��5q�6}   $��80��6�   $	��300��6�   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Gmail��5    %      
Foreground
 � � �5!   %   G� 5%      #��[ 5&      $��30(�5'      $��Center=�6   
�������
��� � �$��5q�6i   $��80��6u   $	��300��6�   $
 Password��5(      
 � � �5)      �� 5-      $��0,20,0,0q�50      $��80��6#   $��30(�6/   $	��300��6=   $��0q�6I   
���  ��  MaterialDesignRaisedButton#��[ 51      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�52         IsIndicatorVisible$
 True��53         IsIndeterminate$
 True��54      $��Log In��55      ��5.      �� *LogInCommand+��5/      �� $
 page��{�57      �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window�� 59      $��Italic$�6   $��1q�6(   $��0,50,0,0q�6<   $��20��6N   
��� � �
������ $��8(�6�   $	��200��6�   #��[ 5:      $ -1Q�5;         IndicatorBackground
 ���� $
 True��5=      $
 True��5>      $��forgot password��5?      {�5A      �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window� 5B      $	��124��5C      .+�5D      �� $��0,30,0,0q�5G        MaterialDesignFlatButton#��[ 5H      $ -1Q�5I      $
 True��5J      $
 True��5K      $��Registration��5L      ��5E      �� *RegistrationCommand+��5F      �� $
 page��{�5M      �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window5N      5O      5P      5Q      5R      