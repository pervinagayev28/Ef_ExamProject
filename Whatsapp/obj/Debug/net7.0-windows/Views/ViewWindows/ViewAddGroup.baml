   M S B A M L   `   `   `  ����C  ?Whatsapp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Whatsapp.Views.ViewWindowsWhatsapp.Views.ViewWindows  5   	   -    'Whatsapp.Views.ViewWindows.ViewAddGroup   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Whatsapp.Views.ViewWindows     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6@   -   5        $��window��5	        � WindowStyle$
  None=�5       � AllowsTransparency$
 True��5
       � WindowStartupLocation$ CenterScreen=�5      
������  � Title$ 	New Group��5   )   $	��500��6;   $	��350��6H    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$	 200��6&   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 0,0,30,0q�6(   �� 5       � ��$ Center=�64   5      5      .��5      ��  �� CornerRadius$ 50��6   ��5   
   �� 5      	 �� ImageSource$	 \Images\background.jpg��6   ._�5   
   � 
������ .+�5#      �� $��20,10q�6f   $��Right=�6v   $	��Top=�6�      MaterialDesignIconButton#��[  5$      
 � ToolTip$
 MaterialDesignIconButton��5%      ��5#      �� *CloseCommand+��66   ��  �� ElementName$ window��{�5&      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window.��5'      '  !MaterialDesignThemes.Wpf.PackIcon  $��50��6*   $��40��66   
�������   Kind$ ContentSave=�5(      5*      �� 5+      .+�5-      �� $��0q�6v   $	��100��6�   $	��100��6�   
������ $��30q�6�   $��0q�6�   ��6   �� *CommandGetImage+��6=   �� $ OpenedImageGrid��.��5.      �� $	��100��6   $	��100��6+   $ 50��67   
���   �$��2q�6^   ��5/      �� 50       j� Stretch$ UniformToFill=�6R   	 6*   �� *Group.ImagePath+6R   52      53      �� 55      $��10q�6    �� Orientation$ Vertical=�6)   $��Center=�6@   .+�56      �� $��
Group Name��6   $��5q�61   �� 57      -   5        $��Password��57   Y   
�������$	��200��61   ��6=   �� *
Group.Name+6Y   58      �� 59      $��10q�6   $ Vertical=�6)   $��Center=�6@   .+�5:      �� $��
Image Path��6   $��5q�61   �� 5;      -   5        $	��asd��5;   ^   
�������$	��200��61   ��6=   �� *Group.ImagePath+6^   5<      �� 5>      $��10q�6F   $	��200��6R   $��30��6^   $��Load Photo from PC��6j   ��6   �� *ChangeImageFromPCCommand+6j   5?      � 5A      -   5        $��OpenedImageGrid��5A      $y�Hidden=�6*   $	��370��6>   .+�5B      �� $��20,10q�6~   $��Right=�6�   $	��Top=�6�   #��[  5C      $
 MaterialDesignIconButton��5D      ��5B      �� *CloseOpenedImageCommand+��6E   �� $ OpenedImageGrid��{�5E      �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window.��5F        $��50��6.   $��40��6:   
��� � �$ Close=�5G      5I      �� 5J      $	��250��6;   $	��300��6H   ��6   �� *Group.ImagePath+6H   5L      5N      5O      5P      