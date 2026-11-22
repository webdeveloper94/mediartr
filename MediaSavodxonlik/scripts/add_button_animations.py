with open('MainWindow.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

start_marker = '<Button x:Name="StartTopicsButton"'
end_marker = '<Border x:Name="StartTopicsCard"'

start_idx = text.find(start_marker)
end_idx = text.find(end_marker)

print("Found markers:", start_idx, end_idx)

replacement = '''<Button x:Name="StartTopicsButton" Tag="topics" Click="Navigate"
                  Margin="0,10,0,0" Padding="0" BorderThickness="0" Background="Transparent"
                  HorizontalAlignment="Left" Cursor="Hand" AutomationProperties.Name="O‘quv mavzulariga o‘tish">
           <Button.Style>
            <Style TargetType="Button">
             <Setter Property="RenderTransform">
              <Setter.Value>
               <TransformGroup>
                <ScaleTransform ScaleX="1" ScaleY="1" CenterX="140" CenterY="35"/>
                <TranslateTransform Y="0"/>
               </TransformGroup>
              </Setter.Value>
             </Setter>
             <Style.Triggers>
              <!-- Hover: yuqoriga -4px ko‘tarilish va yengil 1.02 kattalashish -->
              <EventTrigger RoutedEvent="MouseEnter">
               <BeginStoryboard>
                <Storyboard>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"
                                  To="-4" Duration="0:0:0.18" DecelerationRatio="0.3"/>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"
                                  To="1.02" Duration="0:0:0.18" DecelerationRatio="0.3"/>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"
                                  To="1.02" Duration="0:0:0.18" DecelerationRatio="0.3"/>
                </Storyboard>
               </BeginStoryboard>
              </EventTrigger>
              <EventTrigger RoutedEvent="MouseLeave">
               <BeginStoryboard>
                <Storyboard>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"
                                  To="0" Duration="0:0:0.2" DecelerationRatio="0.3"/>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"
                                  To="1" Duration="0:0:0.2" DecelerationRatio="0.3"/>
                 <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"
                                  To="1" Duration="0:0:0.2" DecelerationRatio="0.3"/>
                </Storyboard>
               </BeginStoryboard>
              </EventTrigger>
              <!-- Click / Pressed holati -->
              <Trigger Property="IsPressed" Value="True">
               <Setter Property="RenderTransform">
                <Setter.Value>
                 <TransformGroup>
                  <ScaleTransform ScaleX="0.97" ScaleY="0.97" CenterX="140" CenterY="35"/>
                  <TranslateTransform Y="1"/>
                 </TransformGroup>
                </Setter.Value>
               </Setter>
              </Trigger>
             </Style.Triggers>
            </Style>
           </Button.Style>
           '''

text = text[:start_idx] + replacement + text[end_idx:]

with open('MainWindow.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("MainWindow.xaml updated successfully!")
