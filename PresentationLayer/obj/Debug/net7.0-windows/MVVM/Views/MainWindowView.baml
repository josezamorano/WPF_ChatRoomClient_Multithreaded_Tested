   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:PresentationLayer.MVVM.ViewsPresentationLayer.MVVM.Views  5   	   U/clr-namespace:PresentationLayer.MVVM.ViewModels!PresentationLayer.MVVM.ViewModels  5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466J6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPFFontAwesome.WPF 5	   	   1    +PresentationLayer.MVVM.Views.MainWindowView   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;view*clr-namespace:PresentationLayer.MVVM.Views     5   	   E	viewModel/clr-namespace:PresentationLayer.MVVM.ViewModels     5   	   X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fLfa_sharp=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   Cfa_wpf6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPF  5	   	     � Title$  MainWindowView��5   	   $	��550��5   	   $	��300��5   	   $	��900��5   	   $	��800��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 0Q�6      ResizeBorderThickness$ 5q�6)    � 	Resources 5       �� DataType=   7PresentationLayer.MVVM.ViewModels.AllChatRoomsViewModel3   -PresentationLayer.MVVM.Views.AllChatRoomsView;   5PresentationLayer.MVVM.ViewModels.ConnectionViewModel1   +PresentationLayer.MVVM.Views.ConnectionView8   2PresentationLayer.MVVM.ViewModels.ContactViewModel.   (PresentationLayer.MVVM.Views.Contactview;   5PresentationLayer.MVVM.ViewModels.InvitationViewModel1	   +PresentationLayer.MVVM.Views.InvitationView?
   9PresentationLayer.MVVM.ViewModels.CreateChatRoomViewModel5   /PresentationLayer.MVVM.Views.CreateChatRoomView?   9PresentationLayer.MVVM.ViewModels.SingleChatRoomViewModel5   /PresentationLayer.MVVM.Views.SingleChatRoomView��%�  (��       5      *, +)(�� 2     5"      *, +)(�� d     5&      *, +)(�� �     5*      *, +)(�� �     5.      *,
 +)(�� �     52      *, +)�� 5   
   "  6   .R�5        5       �� 5"   
   "  6   .R�5#        5$      �� 5&   
   "  6   .R�5'        5(      �� 5*   
   "  6   .R�5+      	  5,      �� 5.   
   " 
 6   .R�5/        50      �� 52   
   "  6   .R�53        54      56      .��58      �� $��Disabled=�59      $
��Auto=�5:      .��5;   
   �� 	 �� CornerRadius$	 5��5<      $��3q�5=      
 J� Opacity$
 1Q�5>      ��5@      �� 5A       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5B      �    panelborderWhite � Color# [  6#    � Offset$ 0Q�6M   � 5C        panelborderBlue_IV# [ 6#   $
 0.35Q�6O   � 5D        panelborderBlue_VI# [ 6#   $
 0.75Q�6O   � 5E        panelborderBlue_I# [ 6#   $ 1Q�6N   5F      ._�5K      �  � RowDefinitions 5L      �� 5N      $��40 �6$   �� 5P      $
��Auto �6$   �� 5R      $��* �6$   ��5T      �� 5U      $ 0.5,0&�6*   $ 0.5,1&�6=   .��5V      �   panelBackgroundGrey_XII# [ 6'   � 5W        panelBackgroundGrey_XIV# [ 6'   $	 0.3Q�6X   � 5X        panelBackgroundGrey_XV# [ 6'   $	 0.6Q�6W   � 5Y        panelBackgroundGrey_XVI# [ 6'   $	 0.9Q�6X   5[      .+�5]      �� $��0��6   
���  ��$��0q�68   �� 5^      $��1��6   
��� � �$��0q�6:   �� 5_      $��2��6   
��� ���$��0q�6;   �� 5c      -   5         ��Name$ WindowControlPanel��5c      $��0��5d       �� Orientation$ 
Horizontal=�5e      $��RightToLeft=�5f      
������ $��0,0,0,0q�5h      .+�5l      �� -   5         ��Name$ ButtonClose��5l        windowPanelButtonCorner#��[ 5m      .��5p      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Xmark=�6-    	 windowPanelIcon#��[	 6:   5q      �� 5s      -   5        $ ButtonMaximize��5s       
 windowPanelButton#��[
 5t      .��5w        $ Square=�6-   #��[	 6;   5x      �� 5z      -   5        $ ButtonMinimize��5z      #��[
 5{      .��5~        $ WindowMinimize=�6-   #��[	 6D   5      5�      �� 5�      -   5         ��Name$" ScrollviewerMenuPanelbuttons��5�      $��1��5�      $
��Auto=�5�      $��Disabled=�5�      .��5�      � -   5         �Name$  GridHeaderMenuPanelButtons��5�      $��1��5�      y�5�      �� *&$GridHeaderMenuPanelButtonsVisibility+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� � ColumnDefinitions 5�      �� 5�      $��* �6/   �� 5�      $
��Auto �6/   �� 5�      $��* �6/   �� 5�      $
��Auto �6/   �� 5�      $��* �6/   �� 5�      $
��Auto �6/   �� 5�      $��* �6/   �� 5�      $
��Auto �6/   �� 5�      $��* �6/   .+�5�      �� -   5        $ ButtonConnection��5�   !   $��1��5�   !     menuPanelButton#��[ 5�   !   ��5�   !   �� *OpenConnectionViewCommand+.��5�      �� $��
Connection��6(   5�      �� 5�      -   5        $ ButtonChatRooms��5�   !   $��3��5�   !   #��[ 5�   !   ��5�   !   �� *OpenChatRoomViewCommand+.��5�      �� $��
Chat Rooms��6(   5�      �� 5�      -	   5        $ ButtonInvitations��5�   !   $��5��5�   !   #��[ 5�   !   ��5�   !   �� *OpenInvitationViewCommand+.��5�      �� $��Invitations��6(   5�      �� 5�      -
   5        $ ButtonContacts��5�   !   $��7��5�   !   #��[ 5�   !   ��5�   !   �� *OpenContactViewCommand+.��5�      �� $��Contacts��6(   5�      5�      5�      �� 5�      -   5         ��Name$ ContentControlChildViews��5�   !   $��1��5�   !   ��5�   !   �� *CurrentChildView+5�      5�      5�      5�      